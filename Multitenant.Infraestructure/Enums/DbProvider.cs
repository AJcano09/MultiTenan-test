using System.ComponentModel;

namespace Multitenant.Infraestructure.Enums;

public enum DbProvider
{
    [Description("UsersAndOrganizations")]
    UsersAndOrganizations,
    
    [Description("Products")]
    Products,
}