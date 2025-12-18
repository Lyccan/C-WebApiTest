var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "GET")
    {
        if (context.Request.Path.StartsWithSegments("/"))
        {
            await context.Response.WriteAsync($"Método {context.Request.Method}{Environment.NewLine}");
            await context.Response.WriteAsync($"a URL é: {context.Request.Path} {Environment.NewLine}");

            var keys = context.Request.Headers.Keys;
            await context.Response.WriteAsync($"Headers: {Environment.NewLine}");
            foreach (var key in keys)
            {
                await context.Response.WriteAsync($"{key}: {context.Request.Headers[key]} {Environment.NewLine}");
            }
        }

        if (context.Request.Path.StartsWithSegments("/carros"))
        {
            await context.Response.WriteAsync($"Lista de carros: {Environment.NewLine}");

            var carros = PopulateCarro.GetCarros();

            foreach (var carro in carros)
            {
                await context.Response.WriteAsync($"Marca: {carro.Marca}, Ano de Fabricação: {carro.AnoFabricacao} {Environment.NewLine}");
            }
        }
    }
});

app.Run();

public class Carro
{
    public int Id { get; set; }
    public string Marca { get; set; }
    public int AnoFabricacao { get; set; }

    public Carro(int id, string marca, int anoFabricacao)
    {
        Id = id;
        Marca = marca;
        AnoFabricacao = anoFabricacao;
    }
}

    static class CarroRepository
{
    private static List<Carro> carros = new List<Carro>
    {
        new Carro(1, "FIAT", 1999),
        new Carro(2, "Wolksvagen", 1945),
        new Carro(3, "Renault", 2007)
    };

    public static List<Carro> GetCarros() => carros;
}



