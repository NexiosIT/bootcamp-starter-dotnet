namespace Orders.Api.Services;

public class OrderNotFoundException(long id) : Exception($"Could not find order {id}");
