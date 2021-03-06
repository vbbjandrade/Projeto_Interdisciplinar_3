﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PI_3.Models;
using Microsoft.EntityFrameworkCore;


namespace PI_3.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(AppDbContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            if(Usuario == null)
            {
                return View("Login");
            }
            
            var professor = _context.Professor.Where(e => e.UsuarioId == Usuario.UsuarioId).ToList();
            if(professor.Count > 0)
            {
                var cursos = _context.Curso.Where(x => x.ProfessorId == professor[0].ProfessorId).ToList();
                var invites = _context.CursoAluno.Include(c => c.Curso).Include(c => c.Aluno).Include(c => c.Aluno.Usuario).Where(x => x.Curso.ProfessorId == professor[0].ProfessorId).ToList();
                ViewBag.Cursos = cursos;
                ViewBag.Invites = invites;
                ViewBag.Usuario = Usuario;
                ViewBag.ProfessorId = professor[0].ProfessorId;
                return View("IndexProf");
            }
            var aluno = _context.Aluno.Where(e => e.UsuarioId == Usuario.UsuarioId).ToList();
            var cursosAluno = _context.CursoAluno.Include(c => c.Curso).Include(c => c.Aluno).Where(x => x.AlunoId == aluno[0].AlunoId).ToList();

            ViewBag.CursoAluno = cursosAluno;
            ViewBag.Usuario = Usuario;
            ViewBag.AlunoId = aluno[0].AlunoId;
            return View("Index");
        }

        public IActionResult Login()
        {
            if(Usuario != null){

                var professor = _context.Professor.Where(e => e.UsuarioId == Usuario.UsuarioId).ToList();
                var aluno = _context.Aluno.Where(e => e.UsuarioId == Usuario.UsuarioId).ToList();

                if(professor.Count > 0){
                    ViewBag.ProfessorId = professor[0].ProfessorId;
                    return Redirect("/");
                }
                ViewBag.AlunoId = aluno[0].AlunoId;
                return Redirect("/");
            }
            return View();
        }

    }
}
