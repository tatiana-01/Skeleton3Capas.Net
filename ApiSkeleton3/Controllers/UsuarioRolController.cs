using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiSkeleton3.Dtos;
using ApiSkeleton3.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiSkeleton3.Controllers;
[ApiVersion("1.0")] //obtener la relacion entre Usuario y rol
[ApiVersion("1.1")] //obtener paginacion, registros y buscador de usuariosRoles
public class UsuarioRolController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UsuarioRolController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Pager<UsuarioRolDto>>> GetPaginaUsuarioRol([FromQuery] Params usuarioParams)
    {
        var usuariosRoles = await _unitOfWork.UsuarioRoles.GetAllAsync(usuarioParams.PageIndex, usuarioParams.PageSize, usuarioParams.Search);

        var lstUsuRolDto = _mapper.Map<List<UsuarioRolDto>>(usuariosRoles.registros);

        return new Pager<UsuarioRolDto>(lstUsuRolDto, usuariosRoles.totalRegistros, usuarioParams.PageIndex, usuarioParams.PageSize, usuarioParams.Search);
    }

    //METODO GET POR ID (Traer un solo registro de la entidad de la  Db)
    [HttpGet("{idUsuario}/{idRol}")]
    //[Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsuarioRolDto>> GetByIdUsuarioRol( int idUsuario, int idRol)
    {
        var usuarioRol = await _unitOfWork.UsuarioRoles.GetByIdAsync(idUsuario, idRol);

        if (usuarioRol == null) {
            return NotFound();
        }

        return _mapper.Map<UsuarioRolDto>(usuarioRol);
    }

    //METODO POST (para enviar registros a la entidad de la Db)
    [HttpPost]
    //[Authorize(Roles = "Administrador")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsuarioRolDto>> Post(UsuarioRolDto usuarioRolDto)
    {
        var usuarioRol = _mapper.Map<UsuarioRol>(usuarioRolDto);
        _unitOfWork.UsuarioRoles.Add(usuarioRol);
        await _unitOfWork.SaveAsync();

        if (usuarioRol == null) {
            return BadRequest();
        }

        return _mapper.Map<UsuarioRolDto>(usuarioRol);
    }

    //METODO PUT (editar un registro de la entidad de la Db)
    [HttpPut("{idUsuario}/{idRol}")]
    //[Authorize(Roles = "Administrador")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsuarioRolDto>> Put(int idUsuario, int idRol, [FromBody] UsuarioRolDto usuarioRolDto)
    {
        if (usuarioRolDto == null) {
            return NotFound();
        }

        var usuarioRol = _mapper.Map<UsuarioRol>(usuarioRolDto);
        usuarioRol.UsuarioId = idUsuario;
        usuarioRol.RolId = idRol;
        _unitOfWork.UsuarioRoles.Update(usuarioRol);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<UsuarioRolDto>(usuarioRol);        
    }

    //METODO DELETE (Eliminar un registro de la entidad de la Db)
    [HttpDelete("{idUsuario}/{idRol}")]
    //[Authorize(Roles = "Administrador")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsuarioRolDto>> Delete(int idUsuario, int idRol)
    {
        var usuarioRol = await _unitOfWork.UsuarioRoles.GetByIdAsync (idUsuario, idRol);
        
        if (usuarioRol == null) {
            return NotFound();
        }

        _unitOfWork.UsuarioRoles.Remove(usuarioRol);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }
}
