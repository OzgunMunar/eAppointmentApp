using eAppointment.WebAPI.Modules;

public static class RouteRegistrar
{
    public static void RegisterRoutes(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.RegisterDoctorModuleRootes();
        endpointRouteBuilder.RegisterPatientModuleRootes();
        endpointRouteBuilder.RegisterAppointmentModuleRootes();
        endpointRouteBuilder.RegisterRolesModuleRootes();
    }
}