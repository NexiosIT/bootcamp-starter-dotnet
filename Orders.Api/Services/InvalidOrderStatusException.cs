namespace Orders.Api.Services;

public class InvalidOrderStatusException(string message) : Exception(message);
