using System.Web.Mvc;
using System.Collections.Generic;
using WebApplication.Models;
using System.Linq;

namespace WebApplication.Controllers
{
    public class PaginaController : Controller
    {
        // GET: Pagina
        public ActionResult Index()
        {

            List<PaginaCLS> listaPagina = new List<PaginaCLS>();

            using( var bd = new BDPasajeEntities())
            {
                listaPagina = (from pagina in bd.Pagina
                               where pagina.BHABILITADO == 1
                               select new PaginaCLS()
                               {
                                   iidpagina = pagina.IIDPAGINA,
                                   mensaje = pagina.MENSAJE,
                                   controlador = pagina.CONTROLADOR,
                                   accion = pagina.ACCION

                               }).ToList();
            }
            return View(listaPagina);
        }

        public ActionResult SearchMessage(PaginaCLS oPaginaCLS)
        {
            string mensaje = oPaginaCLS.mensaje;

            List<PaginaCLS> listaPagina = new List<PaginaCLS>();

            using ( var bd = new BDPasajeEntities())
            {
                if(mensaje == null)
                {
                    listaPagina = (from pagina in bd.Pagina
                                   where pagina.BHABILITADO == 1
                                   select new PaginaCLS()
                                   {
                                       iidpagina = pagina.IIDPAGINA,
                                       mensaje = pagina.MENSAJE,
                                       controlador = pagina.CONTROLADOR,
                                       accion = pagina.ACCION

                                   }).ToList();
                }
                else
                {
                    listaPagina = (from pagina in bd.Pagina
                                   where pagina.BHABILITADO == 1 
                                   && pagina.MENSAJE.Contains(mensaje)
                                   select new PaginaCLS()
                                   {
                                       iidpagina = pagina.IIDPAGINA,
                                       mensaje = pagina.MENSAJE,
                                       controlador = pagina.CONTROLADOR,
                                       accion = pagina.ACCION

                                   }).ToList();
                }

                return PartialView("_TablaPagina", listaPagina);
            }
        }

        public int SavePage(PaginaCLS oPaginaCLS, int operacionPagina)
        {
            int response = 0;

            using(var bd = new BDPasajeEntities())
            {
                if(operacionPagina == 1)
                {
                    Pagina oPagina = new Pagina();
                    oPagina.MENSAJE = oPaginaCLS.mensaje;
                    oPagina.ACCION = oPaginaCLS.accion;
                    oPagina.CONTROLADOR = oPaginaCLS.controlador;
                    oPagina.BHABILITADO = 1;
                    bd.Pagina.Add(oPagina);
                    response = bd.SaveChanges();
                }

            }
            
            return response;
        }
    }
}