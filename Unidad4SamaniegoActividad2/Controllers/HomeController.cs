using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

using Unidad4SamaniegoActividad2.Models;
using Unidad4SamaniegoActividad2.Repositories;
using Unidad4SamaniegoActividad2.Helpers;
using Unidad4SamaniegoActividad2.Models.ViewModels;

namespace Unidad4SamaniegoActividad2.Controllers
{
    
        public class HomeController : Controller
        {
            [Authorize(Roles = "Docente, Director")]
            public IActionResult Index(int nocontrol)
            {
                return View();
            }
        [AllowAnonymous]
        public IActionResult InicioDeSesionDeDirector()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> InicioDeSesionDeDirector(Director dir)
        {
            rolessamaniegoContext context = new rolessamaniegoContext();
            UsuariosRepository<Director> directorRepos = new UsuariosRepository<Director>(context);
            var director = context.Director.FirstOrDefault(x => x.Clave == dir.Clave);
            try
            {
                if (director != null && director.Contrasena == HashingHelper.GetHash(dir.Contrasena))
                {
                    List<Claim> info = new List<Claim>();
                    info.Add(new Claim(ClaimTypes.Name, "Usuario" + director.Nombre));
                    info.Add(new Claim(ClaimTypes.Role, "Director"));
                    info.Add(new Claim("Clave", director.Nombre.ToString()));
                    info.Add(new Claim("Nombre", director.Nombre));

                    var claimsidentity = new ClaimsIdentity(info, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsprincipal = new ClaimsPrincipal(claimsidentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsprincipal,
                        new AuthenticationProperties { IsPersistent = true });
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "La clave o la contraseña del director son incorrectas.");
                    return View(dir);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dir);
            }
        }
        [Authorize(Roles = "Director")]
        public IActionResult RegistrarDocente()
        {
            return View();
        }

        [Authorize(Roles = "Director")]
        [HttpPost]
        public IActionResult RegistrarDocente(Docente dnte)
        {
            rolessamaniegoContext context = new rolessamaniegoContext();
            DocenteRepository repository = new DocenteRepository(context);

            try
            {
                var maestro = repository.GetDocenteByClave(dnte.Clave);
                if (maestro == null)
                {
                    dnte.Activo = 1;
                    dnte.Contrasena = HashingHelper.GetHash(dnte.Contrasena);
                    repository.Insert(dnte);
                    return RedirectToAction("VerDocente");
                }
                else
                {
                    ModelState.AddModelError("", "Clave no está disponible.");
                    return View(dnte);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dnte);
            }
        }



        [AllowAnonymous]
            public IActionResult InicioDeSesionDocente()
            {
                return View();
            }

            [AllowAnonymous]
            [HttpPost]
            public async Task<IActionResult> InicioDeSesionDocente(Docente dnte)
            {
                rolessamaniegoContext context = new rolessamaniegoContext();
                DocenteRepository repository = new DocenteRepository(context);
                var docent = repository.GetDocenteByClave(dnte.Clave);
                try
                {
                    if (docent != null && docent.Contrasena == HashingHelper.GetHash(dnte.Contrasena))
                    {
                        if (docent.Activo == 1)
                        {
                            List<Claim> info = new List<Claim>();
                            info.Add(new Claim(ClaimTypes.Name, "Usuario" + docent.Nombre));
                            info.Add(new Claim(ClaimTypes.Role, "Docente"));
                            info.Add(new Claim("Clave", docent.Clave.ToString()));
                            info.Add(new Claim("Nombre", docent.Nombre));
                            info.Add(new Claim("Id", docent.Id.ToString()));

                            var claimsidentity = new ClaimsIdentity(info, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsprincipal = new ClaimsPrincipal(claimsidentity);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsprincipal,
                                new AuthenticationProperties { IsPersistent = true });
                            return RedirectToAction("Index", docent.Clave);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Atencion, Cuenta desactivada.");
                            return View(dnte);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "La clave o la contraseña del docente son incorrectas.");
                        return View(dnte);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(dnte);
                }
            }
        
            [AllowAnonymous]
            public async Task<IActionResult> CerrarSesion()
            {
                await HttpContext.SignOutAsync();
                return RedirectToAction("Index");
            }

        [Authorize(Roles = "Director")]
        public IActionResult VerDocente()
        {
            rolessamaniegoContext context = new rolessamaniegoContext();
            DocenteRepository dnteRepos = new DocenteRepository(context);
            var docent = dnteRepos.GetAll();
                return View(docent);
            }

            
            [Authorize(Roles = "Director")]
            [HttpPost]
            public IActionResult ActivacionDocente(Docente dnte)
            {
                rolessamaniegoContext context = new rolessamaniegoContext();
                DocenteRepository docenteRepos = new DocenteRepository(context);
                var docent = docenteRepos.GetById(dnte.Id);
                if (docent != null && docent.Activo == 0)
                {
                    docent.Activo = 1;
                    docenteRepos.Edit(docent);
                }
                else
                {
                    docent.Activo = 0;
                    docenteRepos.Edit(docent);
                }
                return RedirectToAction("VerDocente");
            }

            [Authorize(Roles = "Director")]
            public IActionResult EditarDocente(int id)
            {
                rolessamaniegoContext context = new rolessamaniegoContext();
                DocenteRepository docenteRepos = new DocenteRepository(context);
                var docent = docenteRepos.GetById(id);
                if (docent != null)
                {
                    return View(docent);
                }
                return RedirectToAction("VerDocente");
            }

            [Authorize(Roles = "Director")]
            [HttpPost]
            public IActionResult EditarDocente(Docente dnte)
            {
                rolessamaniegoContext context = new rolessamaniegoContext();
                DocenteRepository deocenteRepos = new DocenteRepository(context);
                var docent = deocenteRepos.GetById(dnte.Id);
                try
                {
                    if (docent != null)
                    {
                        docent.Nombre = dnte.Nombre;
                        deocenteRepos.Edit(docent);
                    }
                    return RedirectToAction("VerDocente");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(docent);
                }
            }

            [Authorize(Roles = "Director")]
            public IActionResult CambiarContrasena(int id)
            {
                rolessamaniegoContext context = new rolessamaniegoContext();
                DocenteRepository repository = new DocenteRepository(context);
                var maestro = repository.GetById(id);
                if (maestro == null)
                {
                    return RedirectToAction("VerDocente");
                }
                return View(maestro);
            }

            [Authorize(Roles = "Director")]
            [HttpPost]
            public IActionResult CambiarContrasena(Docente dnte, string nuevaContra, string confirmarContra)
            {
                rolessamaniegoContext context = new  rolessamaniegoContext();
                DocenteRepository docenteRepos = new DocenteRepository(context);
                var docent = docenteRepos.GetById(dnte.Id);
                try
                {

                    if (docent != null)
                    {
                        if (nuevaContra != confirmarContra)
                        {
                            ModelState.AddModelError("", "Las nuevas contraseñas no son iguales.");
                            return View(docent);
                        }
                        else if (docent.Contrasena == HashingHelper.GetHash(nuevaContra))
                        {
                            ModelState.AddModelError("", "Ingreso una contraseña antigua, intentelo con una nueva.");
                            return View(docent);
                        }
                        else
                        {
                            docent.Contrasena = HashingHelper.GetHash(nuevaContra);
                            docenteRepos.Edit(docent);
                            return RedirectToAction("VerDocente");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "El docente al que intentó modificar no existe.");
                        return View(docent);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(docent);
                }
            }

            [Authorize(Roles = "Docente, Director")]
            public IActionResult VerAlumno(int id)
            {
                rolessamaniegoContext context = new rolessamaniegoContext();
                DocenteRepository docenteRepos = new DocenteRepository(context);
                var docent = docenteRepos.GetAlumnoByDocente(id);
                if (docent != null)
                {
                    if (User.IsInRole("Docente"))
                    {
                        if (User.Claims.FirstOrDefault(x => x.Type == "Id").Value == docent.Id.ToString())
                        {
                            return View(docent);
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }
                    else
                    {
                        return View(docent);
                    }
                }
                else
                {
                    return RedirectToAction("VerAlumno");
                }
            }

            [Authorize(Roles = "Docente, Director")]
            public IActionResult AggAlumno(int id)
            {
                rolessamaniegoContext context = new rolessamaniegoContext();
                DocenteRepository docenteRepos = new DocenteRepository(context);
                vmAggAlumno vm = new vmAggAlumno();
                vm.Docente = docenteRepos.GetById(id);
                if (vm.Docente != null)
                {
                    if (User.IsInRole("Docente"))
                    {
                        if (User.Claims.FirstOrDefault(x => x.Type == "Id").Value == vm.Docente.Id.ToString())
                        {
                            return View(vm);
                        }
                        else
                        {
                            return RedirectToAction("Error");
                        }
                    }
                    else
                    {
                        return View(vm);
                    }
                }
                return View(vm);
            }

            [Authorize(Roles = "Docente, Director")]
            [HttpPost]
            public IActionResult AggAlumno(vmAggAlumno vm)
            {
                rolessamaniegoContext context = new rolessamaniegoContext();
                DocenteRepository docenteRepos = new DocenteRepository(context);
                AlumnosRepository alumnosRepos = new AlumnosRepository(context);
            try
            {
                if (context.Alumno.Any(x => x.NumeroDeControl == vm.Alumno.NumeroDeControl))
                {
                    ModelState.AddModelError("", "Este número de control ya se encuentra registrado.");
                    return View(vm);
                }
                else
                {
                    var maestro = docenteRepos.GetDocenteByClave(vm.Docente.Clave).Id;
                    vm.Alumno.IdDocente = maestro;
                    alumnosRepos.Insert(vm.Alumno);
                    return RedirectToAction("VerAlumno", new { id = maestro });
                }

            }
            catch (Exception ex)
            {
                vm.Docente = docenteRepos.GetById(vm.Docente.Id);
                vm.lstDocentes = docenteRepos.GetAll();
                    ModelState.AddModelError("", ex.Message);
                    return View(vm);
                }
            }

        [Authorize(Roles = "Docente, Director")]
        public IActionResult EditarAlumno(int id)
        {
            rolessamaniegoContext context = new rolessamaniegoContext ();
                DocenteRepository docenteRepos = new DocenteRepository(context);
                AlumnosRepository alumnosRepos = new AlumnosRepository(context);
                vmAggAlumno vm = new vmAggAlumno();
                vm.Alumno = alumnosRepos.GetById(id);
                vm.lstDocentes = docenteRepos.GetAll();
                if (vm.Alumno != null)
                {
                    vm.Docente = docenteRepos.GetById(vm.Alumno.Id);
                    if (User.IsInRole("Docente"))
                    {
                        vm.Docente = docenteRepos.GetById(vm.Alumno.IdDocente);
                        if (User.Claims.FirstOrDefault(x => x.Type == "Clave").Value == vm.Docente.Clave.ToString())
                        {

                            return View(vm);
                        }
                    }
                    return View(vm);

                }
                else return RedirectToAction("Index");
            }

            [Authorize(Roles = "Docente, Director")]
            [HttpPost]
            public IActionResult EditarAlumno(vmAggAlumno vm)
            {
                rolessamaniegoContext context = new rolessamaniegoContext();
                DocenteRepository docenteRepos = new DocenteRepository(context);
                AlumnosRepository alumnosRepos = new AlumnosRepository(context);
                try
                {
                    var alumno = alumnosRepos.GetById(vm.Alumno.Id);
                    if (alumno != null)
                    {
                        alumno.Nombre = vm.Alumno.Nombre;
                        alumnosRepos.Edit(alumno);
                        return RedirectToAction("VerAlumno", new { id = alumno.IdDocente });
                    }
                    else
                    {
                        ModelState.AddModelError("", "El alumno que intentó editar no existe.");
                        vm.Docente = docenteRepos.GetById(vm.Alumno.IdDocente);
                        vm.lstDocentes = docenteRepos.GetAll();
                        return View(vm);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    vm.Docente = docenteRepos.GetById(vm.Alumno.IdDocente);
                    vm.lstDocentes = docenteRepos.GetAll();
                    return View(vm);
                }
            }

            [Authorize(Roles = "Docente, Director")]
            [HttpPost]
            public IActionResult EliminarAlumno(Alumno a)
            {
                rolessamaniegoContext context = new rolessamaniegoContext();
                AlumnosRepository alumnosRepos = new AlumnosRepository(context);
                var alumno = alumnosRepos.GetById(a.Id);
                if (alumno != null)
                {
                    alumnosRepos.Delete(alumno);
                }
                else
                {
                    ModelState.AddModelError("", "El alumno que intentó eliminar no existe.");
                }
                return RedirectToAction("VerAlumno", new { id = alumno.IdDocente });
            }

            [AllowAnonymous]
            public IActionResult Error()
            {
                return View();
            }
        }
}
