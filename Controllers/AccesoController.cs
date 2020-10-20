using ProyectoProgramacion.ETL;
using ProyectoProgramacion.Models;
using System;
using System.Web.Mvc;

namespace ProyectoProgramacion.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            if (Session["User"] == null) { return View(); }
            else { return RedirectToAction("Index", "Home"); }

        }

        [HttpPost]
        public ActionResult Index(long User, string Pass)
        {

            try
            {
                UsuarioModelo userModelo = new UsuarioModelo();
                etlUsuario usuario = userModelo.Autenticar(new etlUsuario
                {
                    Empleado = new etlEmpleado
                    {
                        Cedula = User
                    },
                    Password = Pass
                });

                if (usuario == null)
                {
                    ViewBag.Error = "Usuario o password invalida";
                    return View();
                }
                Session["User"] = usuario;


                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }

        }
    }
}