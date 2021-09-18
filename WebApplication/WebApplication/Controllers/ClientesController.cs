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

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ClientesCLS oClientesCLS)
        {

            if(!ModelState.IsValid)
            {
                return View(oClientesCLS);
            }
            else
            {
                using(var bd = new BDPasajeEntities())
                {
                    Cliente oCliente = new Cliente();

                    oCliente.NOMBRE = oClientesCLS.nombre;
                    oCliente.APPATERNO = oClientesCLS.appaterno;
                    oCliente.APMATERNO = oClientesCLS.apmaterno;
                    oCliente.EMAIL = oClientesCLS.email;
                    oCliente.DIRECCION = oClientesCLS.direccion;
                    oCliente.IIDSEXO = oClientesCLS.iidsexo;
                    oCliente.TELEFONOFIJO = oClientesCLS.telefonofijo;
                    oCliente.TELEFONOCELULAR = oClientesCLS.telefonocelular;
                    oCliente.BHABILITADO = 1;

                    bd.Cliente.Add(oCliente);
                    bd.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}