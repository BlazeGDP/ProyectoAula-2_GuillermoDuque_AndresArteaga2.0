using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAula_2_GuillermoDuque_AndresArteaga
{
    public class Departamento
    {
        private static readonly Random random = new Random();
        private static readonly Dictionary<string, string> departamentos = new Dictionary<string, string>();

        public string Nombre { get; private set; }
        public string Codigo { get; private set; }

        private string GenerarCodigoUnico()
        {
            const string caracteresPermitidos = "0123456789ABCD";
            char[] codigo = new char[6];
            for (int i = 0; i < codigo.Length; i++)
            {
                codigo[i] = caracteresPermitidos[random.Next(caracteresPermitidos.Length)];
            }
            return new string(codigo);
        }
        public Departamento(string nombre)
        {
            Nombre = nombre;
            if (!departamentos.TryGetValue(nombre, out string codigo))
            {
                codigo = GenerarCodigoUnico();
                departamentos[nombre] = codigo;
            }
            Codigo = codigo;
        }
    }

    public class IntegranteEquipo
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
    }

    public class IdeaDeNegocio
    {
        private static readonly Random random = new Random();
        public static readonly List<string> herramientas4RI = new List<string>
    {
        "inteligencia artificial",
        "cloud computing",
        "biometría",
        "firma digital"
    };

        public string Nombre { get; private set; }
        public string Codigo { get; private set; }
        public string ImpactoSocialOEconomico { get; set; }
        public List<Departamento> DepartamentosBeneficiarios { get; set; }
        public decimal ValorInversion { get; set; }
        public decimal IngresosPrimeros3Anios { get; set; }
        public List<IntegranteEquipo> IntegrantesDelEquipo { get; set; }
        public string Herramienta4RIUtilizada { get; set; }


        private List<IntegranteEquipo> integrantesEquipo;
        public List<IntegranteEquipo> IntegrantesEquipo
        {
            get { return integrantesEquipo; }
            private set { integrantesEquipo = value; }
        }
        public IdeaDeNegocio(string nombre)
        {
            Nombre = nombre;
            Codigo = GenerarCodigoUnico();
            DepartamentosBeneficiarios = new List<Departamento>();
            integrantesEquipo = new List<IntegranteEquipo>();
        }

        private string GenerarCodigoUnico()
        {
            const string caracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] codigo = new char[6];
            for (int i = 0; i < codigo.Length; i++)
            {
                codigo[i] = caracteresPermitidos[random.Next(caracteresPermitidos.Length)];
            }
            return new string(codigo);
        }
    }

    public class Program
    {
        public static List<IdeaDeNegocio> IdeasNegocio = new List<IdeaDeNegocio>();

        public static void Main()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Agregar una nueva idea de negocio");
                Console.WriteLine("2. Agregar o eliminar integrantes de un equipo");
                Console.WriteLine("3. Modificar valor de inversión y total de ingresos");
                Console.WriteLine("4. Mostrar estadísticas");
                Console.WriteLine("5. Salir");
                Console.WriteLine();
                Console.Write("Seleccione una opción: ");

                int opcion;
                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            AgregarIdeaNegocio();
                            break;
                        case 2:
                            Console.WriteLine("Ingrese el código de la idea de negocio:");
                            string codigoIdea = Console.ReadLine();

                            IdeaDeNegocio ideaEncontrada = IdeasNegocio.FirstOrDefault(idea => idea.Codigo == codigoIdea);

                            if (ideaEncontrada != null)
                            {
                                Console.WriteLine("1. Agregar integrante al equipo");
                                Console.WriteLine("2. Eliminar integrante del equipo");
                                Console.Write("Seleccione una opción: ");

                                if (int.TryParse(Console.ReadLine(), out int opcionIntegrante))
                                {
                                    switch (opcionIntegrante)
                                    {
                                        case 1:
                                            IntegranteEquipo nuevoIntegrante = new IntegranteEquipo();
                                            Console.WriteLine("Ingrese la identificación del nuevo integrante:");
                                            nuevoIntegrante.Identificacion = Console.ReadLine();

                                            Console.WriteLine("Ingrese el nombre del nuevo integrante:");
                                            nuevoIntegrante.Nombre = Console.ReadLine();

                                            Console.WriteLine("Ingrese los apellidos del nuevo integrante:");
                                            nuevoIntegrante.Apellidos = Console.ReadLine();

                                            Console.WriteLine("Ingrese el rol del nuevo integrante dentro del emprendimiento:");
                                            nuevoIntegrante.Rol = Console.ReadLine();

                                            Console.WriteLine("Ingrese el email del nuevo integrante:");
                                            nuevoIntegrante.Email = Console.ReadLine();

                                            AgregarIntegranteEquipo(ideaEncontrada, nuevoIntegrante);

                                            Console.WriteLine("Integrante agregado con éxito al equipo.");
                                            break;
                                        case 2:
                                            Console.WriteLine("Ingrese la identificación del integrante a eliminar:");
                                            string identificacionAEliminar = Console.ReadLine();
                                            bool eliminacionExitosa = EliminarIntegranteEquipo(ideaEncontrada, identificacionAEliminar);

                                            if (eliminacionExitosa)
                                            {
                                                Console.WriteLine("Integrante eliminado con éxito.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("No se encontró al integrante con esa identificación.");
                                            }
                                            break;

                                        default:
                                            Console.WriteLine("Opción no válida.");
                                            break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ingrese un número válido para la opción del integrante.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No se encontró una idea de negocio con ese código.");
                            }
                            break;
                        case 3:
                            ModificarValorEIngresos();
                            break;
                        case 4:
                            MostrarSubMenuEstadisticas();
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("Ingrese un número válido.");
                }
            }
        }

        public static void AgregarIdeaNegocio()
        {
            Console.WriteLine("Ingrese el nombre de la idea de negocio:");
            string nombre = Console.ReadLine();

            IdeaDeNegocio nuevaIdea = new IdeaDeNegocio(nombre);

            Console.WriteLine();
            Console.WriteLine("Ingrese el impacto social o económico:");
            nuevaIdea.ImpactoSocialOEconomico = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Ingrese los departamentos beneficiarios (separados por coma):");
            string departamentosInput = Console.ReadLine();
            string[] departamentosArray = departamentosInput.Split(',');
            foreach (string departamento in departamentosArray)
            {
                nuevaIdea.DepartamentosBeneficiarios.Add(new Departamento(departamento.Trim()));
            }

            Console.WriteLine();
            Console.WriteLine("Ingrese el valor de inversión:");
            if (decimal.TryParse(Console.ReadLine(), out decimal valorInversion))
            {
                nuevaIdea.ValorInversion = valorInversion;
            }

            Console.WriteLine();
            Console.WriteLine("Ingrese el total de ingresos a generar en los primeros 3 años:");
            if (decimal.TryParse(Console.ReadLine(), out decimal ingresosPrimeros3Anios))
            {
                nuevaIdea.IngresosPrimeros3Anios = ingresosPrimeros3Anios;
            }

            Console.WriteLine();
            Console.WriteLine("Ingrese los integrantes del equipo " +
                "(no ingrese identificacion para dejar de agregar participantes):");
            while (true)
            {
                Console.WriteLine("Identificación:");
                string identificacion = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(identificacion))
                {
                    break;
                }

                Console.WriteLine("Nombre:");
                string nombreIntegrante = Console.ReadLine();

                Console.WriteLine("Apellidos:");
                string apellidos = Console.ReadLine();

                Console.WriteLine("Rol dentro del emprendimiento:");
                string rol = Console.ReadLine();

                Console.WriteLine("Email:");
                string email = Console.ReadLine();

                nuevaIdea.IntegrantesEquipo.Add(new IntegranteEquipo
                {
                    Identificacion = identificacion,
                    Nombre = nombreIntegrante,
                    Apellidos = apellidos,
                    Rol = rol,
                    Email = email
                });
            }

            string herramienta;
            bool herramientaValida = false;

            Console.WriteLine();
            Console.WriteLine("Elija una herramienta de la 4RI utilizada " +
                "(inteligencia artificial, cloud computing, biometría, firma digital):");
            while (!herramientaValida)
            {
                herramienta = Console.ReadLine().ToLower();

                if (IdeaDeNegocio.herramientas4RI.Contains(herramienta))
                {
                    nuevaIdea.Herramienta4RIUtilizada = herramienta;
                    herramientaValida = true;
                }
                else
                {
                    Console.WriteLine("Herramienta no válida. Selecciona una valida " +
                        "(inteligencia artificial, cloud computing, biometría, firma digital): ");

                }

            }
            IdeasNegocio.Add(nuevaIdea);
            Console.WriteLine();
            Console.WriteLine($"Idea añadida con Exito, el codigo de tu idea es: {nuevaIdea.Codigo} no lo pierdas!!! ");
        }

        public static void AgregarIntegranteEquipo(IdeaDeNegocio idea, IntegranteEquipo integrante)
        {
            idea.IntegrantesEquipo.Add(integrante);
        }
        public static bool EliminarIntegranteEquipo(IdeaDeNegocio idea, string identificacion)
        {
            IntegranteEquipo integranteAEliminar = idea.IntegrantesEquipo.FirstOrDefault(x => x.Identificacion == identificacion);

            if (integranteAEliminar != null)
            {
                idea.IntegrantesEquipo.Remove(integranteAEliminar);
                return true;
            }
            return false;
        }

        public static void ModificarValorEIngresos()
        {
            Console.WriteLine("Ingrese el código de la idea de negocio a modificar:");
            string codigoIdea = Console.ReadLine();

            IdeaDeNegocio ideaEncontrada = IdeasNegocio.FirstOrDefault(idea => idea.Codigo == codigoIdea);

            if (ideaEncontrada != null)
            {
                Console.WriteLine("Valor de inversión actual: " + ideaEncontrada.ValorInversion);
                Console.WriteLine("Total de ingresos actual en los primeros 3 años: " + ideaEncontrada.IngresosPrimeros3Anios);

                Console.WriteLine();
                Console.WriteLine("Ingrese el nuevo valor de inversión:");
                if (decimal.TryParse(Console.ReadLine(), out decimal nuevoValorInversion))
                {
                    ideaEncontrada.ValorInversion = nuevoValorInversion;
                    Console.WriteLine("Valor de inversión modificado con éxito.");
                }
                else
                {
                    Console.WriteLine("Ingrese un valor de inversión válido.");
                }
                Console.WriteLine();
                Console.WriteLine("Ingrese el nuevo total de ingresos en los primeros 3 años:");
                if (decimal.TryParse(Console.ReadLine(), out decimal nuevosIngresos))
                {
                    ideaEncontrada.IngresosPrimeros3Anios = nuevosIngresos;
                    Console.WriteLine("Total de ingresos modificado con éxito.");
                }
                else
                {
                    Console.WriteLine("Ingrese un total de ingresos válido.");
                }
            }
            else
            {
                Console.WriteLine("No se encontró una idea de negocio con ese código.");
            }
        }
        public static void MostrarInformacionIdea(IdeaDeNegocio idea)
        {
            Console.WriteLine("Nombre: " + idea.Nombre);
            Console.WriteLine("Código: " + idea.Codigo);
            Console.WriteLine("Impacto Social o Económico: " + idea.ImpactoSocialOEconomico);
            Console.WriteLine("Valor de Inversión: " + idea.ValorInversion);
            Console.WriteLine("Total de Ingresos en los Primeros 3 Años: " + idea.IngresosPrimeros3Anios);

            Console.WriteLine("Departamentos Beneficiarios:");
            foreach (var departamento in idea.DepartamentosBeneficiarios)
            {
                Console.WriteLine("- Nombre: " + departamento.Nombre);
                Console.WriteLine("- Código: " + departamento.Codigo);
            }

            Console.WriteLine("Integrantes del Equipo:");
            foreach (var integrante in idea.IntegrantesEquipo)
            {
                Console.WriteLine("- Identificación: " + integrante.Identificacion);
                Console.WriteLine("- Nombre: " + integrante.Nombre);
                Console.WriteLine("- Apellidos: " + integrante.Apellidos);
                Console.WriteLine("- Rol: " + integrante.Rol);
                Console.WriteLine("- Email: " + integrante.Email);
            }

            Console.WriteLine("Herramienta 4RI Utilizada: " + idea.Herramienta4RIUtilizada);
        }


        public static void MostrarMayorImpactoYMayorIngresos()
        {
            IdeaDeNegocio ideaMayorImpacto = 
                IdeasNegocio.OrderByDescending(idea => idea.DepartamentosBeneficiarios.Count).FirstOrDefault();
            IdeaDeNegocio ideaMayorIngresos = 
                IdeasNegocio.OrderByDescending(idea => idea.IngresosPrimeros3Anios).FirstOrDefault();

            if (ideaMayorImpacto != null)
            {
                Console.WriteLine("Idea de negocio con mayor impacto en departamentos:");
                MostrarInformacionIdea(ideaMayorImpacto);
            }

            if (ideaMayorIngresos != null)
            {
                Console.WriteLine("Idea de negocio con mayor total de ingresos en los primeros 3 años:");
                MostrarInformacionIdea(ideaMayorIngresos);
            }
        }

        public static void MostrarTop3Rentables()
        {
            var ideasRentables = 
                IdeasNegocio.OrderByDescending(idea => idea.IngresosPrimeros3Anios - idea.ValorInversion).Take(3);

            Console.WriteLine("Las 3 ideas de negocio más rentables son:");
            foreach (var idea in ideasRentables)
            {
                Console.WriteLine(idea.Nombre);
            }
        }

        public static void MostrarImpactoMasDeTresDepartamentos()
        {
            var ideasImpactoMasDeTresDepartamentos = 
                IdeasNegocio.Where(idea => idea.DepartamentosBeneficiarios.Count > 3);

            Console.WriteLine("Ideas de negocio que impactan más de 3 departamentos:");
            foreach (var idea in ideasImpactoMasDeTresDepartamentos)
            {
                Console.WriteLine(idea.Nombre);
            }
        }

        public static void MostrarSumaTotalIngresos()
        {
            decimal sumaTotalIngresos = IdeasNegocio.Sum(idea => idea.IngresosPrimeros3Anios);

            Console.WriteLine("Suma total de ingresos de todas las ideas de negocio: " + sumaTotalIngresos);
        }

        public static void MostrarSumaTotalInversion()
        {
            decimal sumaTotalInversion = IdeasNegocio.Sum(idea => idea.ValorInversion);

            Console.WriteLine("Suma total de la inversión en todas las ideas de negocio: " + sumaTotalInversion);
        }

        public static void MostrarMayorCantidadHerramientas4RI()
        {
            IdeaDeNegocio ideaMayorHerramientas = 
                IdeasNegocio.OrderByDescending(idea => idea.IntegrantesEquipo.Count
                (equipo => IdeaDeNegocio.herramientas4RI.Contains(equipo.Rol))).FirstOrDefault();

            if (ideaMayorHerramientas != null)
            {
                Console.WriteLine("Idea de negocio con la mayor cantidad de herramientas 4RI:");
                MostrarInformacionIdea(ideaMayorHerramientas);
            }
        }

        public static void MostrarCantidadIdeasConIA()
        {
                int contador = 0;

                foreach (var idea in IdeasNegocio)
                {
                    if (idea.Herramienta4RIUtilizada == "inteligencia artificial")
                    {
                        contador++;
                    Console.WriteLine("La cantidad de ideas con IA son: " + contador);
                    }
                }
                
        }

        public static void MostrarSubMenuEstadisticas()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Seleccione una opción de estadísticas:");
                Console.WriteLine("1. Mostrar idea de negocio con mayor impacto y mayor ingreso");
                Console.WriteLine("2. Mostrar las 3 ideas de negocio más rentables");
                Console.WriteLine("3. Mostrar ideas que impactan más de 3 departamentos");
                Console.WriteLine("4. Mostrar suma total de ingresos");
                Console.WriteLine("5. Mostrar suma total de inversión");
                Console.WriteLine("6. Mostrar idea con mayor cantidad de herramientas 4RI");
                Console.WriteLine("7. Mostrar cantidad de ideas con Inteligencia Artificial");
                Console.WriteLine("8. Volver al menú principal");
                Console.WriteLine();

                int opcionEstadisticas;
                if (int.TryParse(Console.ReadLine(), out opcionEstadisticas))
                {
                    switch (opcionEstadisticas)
                    {
                        case 1:
                            MostrarMayorImpactoYMayorIngresos();
                            break;
                        case 2:
                            MostrarTop3Rentables();
                            break;
                        case 3:
                            MostrarImpactoMasDeTresDepartamentos();
                            break;
                        case 4:
                            MostrarSumaTotalIngresos();
                            break;
                        case 5:
                            MostrarSumaTotalInversion();
                            break;
                        case 6:
                            MostrarMayorCantidadHerramientas4RI();
                            break;
                        case 7:
                            MostrarCantidadIdeasConIA();
                            break;
                        case 8:
                            return;
                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ingrese un número válido.");
                }
            }
        }
    }
}
