using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class RolPaginaController : Controller
    {
        // GET: RolPagina
        public ActionResult Index()
        {
            List<RolPaginaCLS> listaRol = new List<RolPaginaCLS>();

            using( var bd = new BDPasajeEntities())
            {
                listaRol = (from rolpagina in bd.RolPagina
                              join Rol in bd.Rol
                              on rolpagina.IIDROL equals Rol.IIDROL
                              join pagina in bd.Pagina
                              on rolpagina.IIDPAGINA equals pagina.IIDPAGINA
                              select new RolPaginaCLS
                              {
                                  iidRolPagina = rolpagina.IIDROLPAGINA,
                                  nombreRol = Rol.NOMBRE,
                                  nombreMensaje = pagina.MENSAJE
                              }).ToList();
            }

            return View(listaRol);
        }
    }
}