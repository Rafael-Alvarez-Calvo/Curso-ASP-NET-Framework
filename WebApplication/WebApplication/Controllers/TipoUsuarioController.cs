using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class TipoUsuarioController : Controller
    {
        private TipoUsuarioCLS oTipoVal;
        private bool SearchUserType(TipoUsuarioCLS oTipoUsuarioCLS)
        {
            bool busquedaId = true;
            bool busquedaNombre= true;
            bool busquedaDescripcion = true;

            if (oTipoVal.iidtipousuario > 0)
                busquedaId = oTipoUsuarioCLS.iidtipousuario.ToString().Contains(oTipoVal.iidtipousuario.ToString());
            
            if (oTipoVal.nombre != null)
                busquedaNombre = oTipoUsuarioCLS.nombre.ToString().ToLower().Contains(oTipoVal.nombre.ToLower());
            
            if (oTipoVal.descripcion != null)
                busquedaDescripcion = oTipoUsuarioCLS.descripcion.ToString().ToLower().Contains(oTipoVal.descripcion.ToLower());

            return (busquedaId && busquedaNombre && busquedaDescripcion);
        }

        // GET: TipoUsuario
        public ActionResult Index(TipoUsuarioCLS oTipoUsuario)
        {
            oTipoVal = oTipoUsuario;

            List<TipoUsuarioCLS> listaTipoUsuario = null;
            List<TipoUsuarioCLS> listaFiltrada;

            using( var BD = new BDPasajeEntities())
            {
                listaTipoUsuario = (from tipoUsuario in BD.TipoUsuario
                                    where tipoUsuario.BHABILITADO == 1
                                    select new TipoUsuarioCLS
                                    {
                                        iidtipousuario = tipoUsuario.IIDTIPOUSUARIO,
                                        nombre = tipoUsuario.NOMBRE,
                                        descripcion = tipoUsuario.DESCRIPCION,

                                    }).ToList();

                if(oTipoUsuario.iidtipousuario == 0 && oTipoUsuario.nombre == null && oTipoUsuario.descripcion == null)
                {
                    listaFiltrada = listaTipoUsuario;
                }
                else
                {
                    Predicate<TipoUsuarioCLS> pred = new Predicate<TipoUsuarioCLS>(SearchUserType);
                    listaFiltrada = listaTipoUsuario.FindAll(pred);
                }
            }

            return View(listaFiltrada);
        }
    }
}