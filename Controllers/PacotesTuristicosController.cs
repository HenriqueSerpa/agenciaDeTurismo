using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using atv02_henriqueSerpa.Models;
using Microsoft.AspNetCore.Http;

namespace atv02_henriqueSerpa.Controllers
{
    public class PacotesTuristicosController : Controller
    {
        public IActionResult Registrar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registrar(PacotesTuristicos novoPacote)
        {
            PacotesTuristicosRepository ptr = new PacotesTuristicosRepository();
            novoPacote.Usuario = (int)(HttpContext.Session.GetInt32("IdUsuario"));
            ptr.RegistrarPacote(novoPacote);
            ViewData["mensagem"] = "Pacote registrado com sucesso!";
            return View();
        }

        public IActionResult ListaPacotes()
        {
            PacotesTuristicosRepository ptr = new PacotesTuristicosRepository();
            return View(ptr.Listar());
        }

        public IActionResult AlterarPacote(int Id)
        {
            PacotesTuristicosRepository ptr = new PacotesTuristicosRepository();
            PacotesTuristicos pacoteBuscado = ptr.BuscarId(Id);
            return View(pacoteBuscado);
        }
        [HttpPost]
        public IActionResult AlterarPacote(PacotesTuristicos pacote)
        {
            PacotesTuristicosRepository ptr = new PacotesTuristicosRepository();
            ptr.Alterar(pacote);
            return RedirectToAction("ListaPacotes");
        }

        public IActionResult Remover(int Id)
        {
            PacotesTuristicosRepository ptr = new PacotesTuristicosRepository();
            PacotesTuristicos pacoteBuscado = ptr.BuscarId(Id);
            ptr.Remover(pacoteBuscado);
            return RedirectToAction("ListaPacotes");
        }
    }
}