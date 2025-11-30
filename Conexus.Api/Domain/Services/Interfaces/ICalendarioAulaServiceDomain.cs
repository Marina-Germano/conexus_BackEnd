using Conexus.Api.Aplication;
using Conexus.Api.Aplication.DTOs;
using Conexus.Api.Domain.Models;

namespace Conexus.Api.Domain.Services.Interfaces;

public interface ICalendarioAulaServiceDomain
{
    Task<ApplicationResult<long>> Inserir(CalendarioAulaDTO calendarioAulaDTO);

    Task<ApplicationResult<CalendarioAulaDTO>> Atualizar(CalendarioAulaDTO calendarioAulaDTO);

    Task<ApplicationResult<CalendarioAulaDTO>> Excluir(int id);

    Task<ApplicationResult<List<CalendarioAulaDTO>>> BuscarTodos();
}