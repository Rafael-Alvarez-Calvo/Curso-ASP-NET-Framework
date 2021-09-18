using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            List<ClientesCLS> listaClientes = null;

            using (var bd = new BDPasajeEntities())
            {
                listaClientes = (from clientes in bd.Cliente
                                 where clientes.BHABILITADO == 1
                                 select new ClientesCLS
                                 {
                                     iidcliente = clientes.IIDCLIENTE,
                                     nombre = clientes.NOMBRE,
                                     appaterno = clientes.APPATERNO,
                                     apmaterno = clientes.APMATERNO,
                                     email = clientes.EMAIL,
                                     direccion = clientes.DIRECCION,
                                     iidsexo = (int)clientes.IIDSEXO,
                                     telefonofijo = clientes.TELEFONOFIJO,
                                     telefonocelular = clientes.TELEFONOCELULAR,

                                 }).ToList();
            }
                return View(listaClientes);
        }
    }
}