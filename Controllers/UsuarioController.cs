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
    public class UsuarioController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Cadastro(Usuario novoUsuario)
        {
            UsuarioRepository ur = new UsuarioRepository();
            ur.Inserir(novoUsuario);
            ViewData["mensagem"] = "Cadastro realizado com sucesso!";
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Usuario novoAcesso)
        {
            UsuarioRepository ur = new UsuarioRepository();
            ur.validarLogin(novoAcesso);
            Usuario usuarioSessao = ur.validarLogin(novoAcesso);

            if(usuarioSessao==null)
            {
                ViewBag.mensagem = "Usuário não cadastrado.";
                return View();
            } else {
                ViewBag.mensagem = "Você entrou com sucesso!";
                HttpContext.Session.SetInt32("IdUsuario", usuarioSessao.Id);
                HttpContext.Session.SetString("NomeUsuario", usuarioSessao.Nome);
                return View();
            }
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}