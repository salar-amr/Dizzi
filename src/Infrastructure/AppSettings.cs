namespace Infrastructure;

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { set; get; } = new ConnectionStrings();

    public JwtTokenConfig JwtTokenConfig { set; get; } = new JwtTokenConfig();

    public RabbitMQConfig RabbitMQConfig { set; get; } = new RabbitMQConfig();
}

public class ConnectionStrings
{
    public string MySqlConnection { set; get; } = string.Empty;

    public string RedisConnection { set; get; } = string.Empty;

}

public class JwtTokenConfig
{
    public string Secret { set; get; } = string.Empty;
}

public class RabbitMQConfig
{
    public string Host { set; get; } = string.Empty;
    public string UserName { set; get; } = string.Empty;
    public string Password { set; get; } = string.Empty;

}
