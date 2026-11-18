namespace Orders.Api.Services;

public class InvalidOrderException(string message) : Exception(message);
