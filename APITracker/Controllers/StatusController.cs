using APITracker.Data.DTO;
using APITracker.Models;
using AutoMapper;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;

namespace APITracker.Controllers;

public class StatusController : ApiController
{
    private readonly IMapper _mapper;

    public StatusController(IMapper mapper)
    {
        _mapper = mapper;
    }
    [HttpGet]
    public IHttpActionResult VerificarStatus(string endereco)
    {
        try
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(endereco).Result;
                HttpStatusCode statusCode = response.StatusCode;

                stopwatch.Stop();
                TimeSpan tempoDuracao = stopwatch.Elapsed;

                //Requisicao requisicao = ObterRequisicaoDoBancoDeDados();

                RequisicaoDTO requisicaoDTO = _mapper.Map<RequisicaoDTO>(endereco);

                if (tempoDuracao > TimeSpan.FromSeconds(10))
                {
                    return Ok(new { StatusCode = statusCode, Duracao = tempoDuracao, Mensagem = "A requisição demorou mais de 10 segundos." });
                }

                if (tempoDuracao > TimeSpan.FromSeconds(15))
                {
                    return Ok(new { StatusCode = statusCode, Duracao = tempoDuracao, Mensagem = "A requisição demorou mais de 15 segundos." });
                }
                if (tempoDuracao > TimeSpan.FromSeconds(60))
                {
                    return Ok(new { StatusCode = statusCode, Duracao = tempoDuracao, Mensagem = "A requisição demorou mais de 60 segundos." });
                }

                return Ok(new { StatusCode = statusCode, Duracao = tempoDuracao });
            }
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
}

