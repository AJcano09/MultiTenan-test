namespace MultiTenant.Application.Dtos;

public class UserDto
{
    public int Status { get; set;}

    public string StatusText { get; set; }

    public Data Data { get; set; }
}

public class Data
{
    public string AccesToken { get; set; }
    public IList<Tenant> Tenants { get; set; }
}

public class Tenant
{
    public string SlugTenant { get; set; }
}