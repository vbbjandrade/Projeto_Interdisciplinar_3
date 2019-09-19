using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PI_3.Models;

namespace PI_3.Controllers.API {
    [Route("api/[controller]")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        public AppDbContext _context;

        public ArquivoController (AppDbContext context)
        {
            return _context.Arquivo.ToList();
        }

        [HttpPost]
        public ActionResult<Arquivo> AddArquivo(Arquivo requestArquivo)
        {
            if (requestArquivo == null)
                return null;

            Arquivo arquivo = new Arquivo();
            arquivo.ArquivoTipo = requestArquivo.ArquivoTipo;
            arquivo.ArquivoUrl = requestArquivo.ArquivoUrl;

            _context.Arquivo.Add(arquivo);
            _context.SaveChanges();

            return arquivo;
        }

        [HttpPut("{id}")]
        public ActionResult UpdateArquivo(int id, Arquivo requestArquivo)
        {
            if (id != requestArquivo.ArquivoId)
                return BadRequest();

            var arquivo = _context.Arquivo.SingleOrDefault(x => x.ArquivoId == requestArquivo.ArquivoId);
            arquivo.ArquivoTipo = requestArquivo.ArquivoTipo;
            arquivo.ArquivoUrl = requestArquivo.ArquivoUrl;

            _context.Arquivo.Update(arquivo);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteArquivo(int id) {
            var arquivo = _context.Arquivo.SingleOrDefault(x => x.ArquivoId == id);

            if (curso == null)
                return NotFound();

            _context.Arquivo.Remove(arquivo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}