using APITracker.Data.DTO;
using APITracker.Entities;
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
    public IActionResult RegistrarStatusRealTime([FromQuery] RequisicaoDTO requisicaoDTO)
    {
        try
        {
            using HttpClient client = new();

            client.Timeout = TimeSpan.FromMinutes(requisicaoDTO.TimeOutEmMinutos);

            Stopwatch stopwatch = new();
            stopwatch.Start();

            HttpResponseMessage response = null;

            if (requisicaoDTO.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                StringContent json = new(
                    content: requisicaoDTO.Body,
                    encoding: Encoding.UTF8,
                    mediaType: "application/json");

                response = client.PostAsync(requisicaoDTO.Endereco, json).Result;
            }
            else if (requisicaoDTO.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                response = client.GetAsync(requisicaoDTO.Endereco).Result;
            }

            stopwatch.Stop();

            TimeSpan duration = stopwatch.Elapsed;

            HttpStatusCode statusCode = response.StatusCode;

            string error = string.Empty;

            if (statusCode == HttpStatusCode.BadRequest || statusCode == HttpStatusCode.InternalServerError)
            {
                error = response.Content.ReadAsStringAsync().Result;
            }

            return Ok(new
            {
                StatusCode = statusCode,
                Duracao = duration.Seconds,
                Error = error
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    [HttpPost]
    [Route("api/status")]
    public async Task<IActionResult> RegistrarStatus([FromQuery] RequisicaoDTO requisicaoDTO)
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
    public async Task<IActionResult> VerificarStatusPorAmbiente(string ambiente)
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

