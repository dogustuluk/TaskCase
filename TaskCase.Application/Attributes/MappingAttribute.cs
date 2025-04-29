namespace TaskCase.Application.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class CommandRequestMappingAttribute : Attribute
{
}
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class CommandResponseMappingAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class QueryRequestMappingAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class QueryResponseMappingAttribute : Attribute
{
}

