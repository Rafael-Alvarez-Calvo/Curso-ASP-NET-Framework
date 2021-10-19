using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {
            List<RolCLS> listaRol = new List<RolCLS>();

            using( var bd = new BDPasajeEntities())
            {
                
                listaRol = (from rol in bd.Rol
                            where rol.BHABILITADO == 1
                            select new RolCLS
                            {
                                iidRol = rol.IIDROL,
                                nombre = rol.NOMBRE,
                                descripcion = rol.DESCRIPCION,

                            }).ToList();
            }

            return View(listaRol);
        }

        [HttpPost]
        public ActionResult SearchRole(string nombre)
        {
            List<RolCLS> listaRol = new List<RolCLS>();

            using (var bd = new BDPasajeEntities())
            {
                if(nombre == null)
                {
                    listaRol = (from rol in bd.Rol
                                where rol.BHABILITADO == 1
                                select new RolCLS
                                {
                                    iidRol = rol.IIDROL,
                                    nombre = rol.NOMBRE,
                                    descripcion = rol.DESCRIPCION,

                                }).ToList();
                }
                else
                {
                    listaRol = (from rol in bd.Rol
                                where rol.BHABILITADO == 1
                                && rol.NOMBRE.Contains(nombre)
                                select new RolCLS
                                {
                                    iidRol = rol.IIDROL,
                                    nombre = rol.NOMBRE,
                                    descripcion = rol.DESCRIPCION,

                                }).ToList();
                }

                return PartialView("_TablaRol", listaRol);
            }
        }

        public int SaveRole(RolCLS oRolCLS, int titulo)
        {
            int response = 0;

            using( var bd = new BDPasajeEntities())
            {
                if(titulo == 1)
                {
                    Rol oRol = new Rol();
                    oRol.NOMBRE = oRolCLS.nombre;
                    oRol.DESCRIPCION = oRolCLS.descripcion;
                    oRol.BHABILITADO = 1;
                    bd.Rol.Add(oRol);
                    response = bd.SaveChanges();
                }
            }

            return response;
        }
    }
}