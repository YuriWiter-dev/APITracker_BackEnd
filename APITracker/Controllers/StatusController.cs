using APITracker.Data.DTO;
using APITracker.Entities;
using APITracker.Enums;
using APITracker.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text;
using static System.Net.WebRequestMethods;

namespace APITracker.Controllers;

[ApiController]
[Route("")]
public class StatusController : Controller
{
    private readonly IMapper _mapper;
    private readonly IEnderecoApiRepository _enderecoApiRepository;

    public StatusController(IMapper mapper, IEnderecoApiRepository enderecoApiRepository)
    {
        _mapper = mapper;
        _enderecoApiRepository = enderecoApiRepository;
    }

    [HttpPost]
    [Route("api/status/realtime")]
    public async Task<IActionResult> RegistrarStatusRealTime()
    {
        try
        {
            IEnumerable<EnderecoApi> enderecos = await _enderecoApiRepository.BuscarTodos();

            foreach (EnderecoApi endereco in enderecos)
            {
                using HttpClient client = new();

                client.Timeout = endereco.TimeOutEmMinutos <= 0 ? TimeSpan.FromSeconds(20) : TimeSpan.FromMinutes(endereco.TimeOutEmMinutos);

                Stopwatch stopwatch = new();
                stopwatch.Start();

                HttpResponseMessage response = null;

                if (endereco.Method.Equals(Method.POST))
                {
                    StringContent json = new(
                        content: endereco.Body,
                        encoding: Encoding.UTF8,
                        mediaType: "application/json");

                    response = client.PostAsync(endereco.Endereco, json).Result;
                }
                else if (endereco.Method.Equals(Method.GET))
                {
                    response = client.GetAsync(endereco.Endereco).Result;
                }

                stopwatch.Stop();

                TimeSpan duration = stopwatch.Elapsed;

                HttpStatusCode statusCode = response.StatusCode;

                string error = string.Empty;

                if (statusCode == HttpStatusCode.BadRequest || statusCode == HttpStatusCode.InternalServerError)
                {
                    error = response.Content.ReadAsStringAsync().Result;
                }

                endereco.StatusCode = (int)statusCode;
                endereco.TimeOutEmMinutos = duration.Seconds;
                endereco.Error = error;

                await _enderecoApiRepository.Alterar(endereco);
            }

            return Ok();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    [HttpPost]
    [Route("api/inserir")]
    public async Task<IActionResult> RegistrarStatus([FromBody] RequisicaoDTO requisicaoDTO)
    {
        try
        {
            EnderecoApi enderecoApi = _mapper.Map<EnderecoApi>(requisicaoDTO);
            await _enderecoApiRepository.Incluir(enderecoApi);
            return Ok();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    [HttpGet]
    [Route("api/status")]
    public async Task<IActionResult> VerificarStatus()
    {
        try
        {
            return Ok(_mapper.Map<IEnumerable<StatusDTO>>(await _enderecoApiRepository.BuscarTodos()));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    [HttpGet]
    [Route("api/status/ambiente")]
    public async Task<IActionResult> VerificarStatusPorAmbiente(Ambiente ambiente)
    {
        try
        {

            var enderecos = await _enderecoApiRepository.BuscarTodosComPesquisa(c => c.Ambiente == ambiente);

            return Ok(_mapper.Map<IEnumerable<StatusDTO>>(enderecos));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

