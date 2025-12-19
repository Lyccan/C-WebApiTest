O projeto representa uma web API em C# feita a partir do objeto HttpContext, este tem o intuito de entender e descrever melhor o funcionamento de APIs no .NET. Este projeto possui um intuito acadêmico.

Alguns detalhes sobre o código:
Tratamento de erros; 
Métodos GET, POST, PUT e DELETE;
Feito a partir de memória, sem persistência de dados;
Necessário utilização de ferramentas para teste da rotas (como Postman ou SwaggerAPI).

Aplicação dos métodos:

GET:
O método possui duas URLs disponíveis, sendo a "/" e "/carros", sendo a URL de index responsável por retornar o método utilizado para acessar (GET) e os headers da requisição.
Já a URL "/carros" é responsável por responder ao browser uma lista de carros criadas em memória, onde a mesma pode ser atualizada com os métodos adequados.

POST:
O método só possui um caminho de acesso, sendo este o "/carros", responsável por, a partir do body da requisição criar um objeto em memória (que some quando há um reload no programa).
 possui alguns tratamentos de erros para não serem criados instâncias não nulas na memória e também não haver paralisação do programa.

PUT:
É importante ressaltar de começo que o método put pode ser implementado de duas formas diferentes, sendo estas - query String e corpo da requisição - Neste projeto em específico escolhi a segunda forma, 
isto porquê, escolhi a query string para ser implementado no método subsequente, DELETE.
Este método possui uma peculiaridade comparado aos demais, é necessário um identificador para o determinado objeto ser atualizado, por isso é importante, na criação do objeto Carro, implementar a propriedade id.
Então, subsequentemente, haverá um paramêtro na função de update para encontrar este identificador. Ademais o método também possui tratamento de erros, e reside na URL "/carros", a partir da requisição feita no
corpo da página, o carro do devido ID é então atualizado para os valores especificados no corpo da requisição.
Em caso de preferência de desenvolver com o query string, o exemplo do DELETE pode ser implementado com poucas modificações neste método.

DELETE:
Este método, assim como o anterior pode ser implementado com corpo da requisição ou query string, como dito anteriormente, optei pela query string, pelo fator de diversificação no código, conseguindo estender
a informação passada por este projeto.
Para a query string ser implementada, deve-se passar o id correspondente na url, como no exemplo a seguir "http://localhost:5067/carros?id=1", acho de devida importância ser ressaltado que, a implementação
deste método é mais fácil, além de eficiente.
Esta requisição também possui seu devido tratamento de erros para certifcar-se de que a requisição não é nula e o ID existe.
Em caso de preferência de desenvolver com a requisição estando no corpo, pode utilizar-se o seguinte código:

    ```if(context.Request.Method == "DELETE")
    {

    if(context.Request.Path.StartsWithSegments("/carros"))
    {

        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();

        if(string.IsNullOrWhiteSpace(body))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Request vazio.");
            return;
        }

        int? carroId = System.Text.Json.JsonSerializer.Deserialize<Carro>(body).Id;
        if (carroId is null || carroId == 0)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("O valor do ID não pode ser nulo ou 0.");
            return;
        }

        bool deleted = CarroRepository.DeleteCarro(carroId);
        context.Response.StatusCode = deleted ? StatusCodes.Status200OK : StatusCodes.Status404NotFound;
        await context.Response.WriteAsync(deleted ? "Objeto deletado" : "Objeto não encontrado");


    }
```}```


Exemplos de requisição JSON para teste da API:

O método GET pode ser acesso por duas URLs, sendo

```"localhost:5067/"``` e também ```"localhost:5067```
esse tipo de requisição não necessita necessariamente de uma ferramenta de testes e pode ser acessada diretamente pelo browser;

É importante ressaltar que deve ser utilizada uma ferramenta como Insomnia, Postman ou SwaggerAPI para realizar os tipos de requisição subsequentes.

Método POST:
Primeiro selecione "POST" na sua ferramenta de teste, e então coloque no corpo da requisição JSON:
```"Id": 4``` ou numero de preferência;
```"Marca": "FIAT"``` ou marca de preferência;
```"AnoFabricacao": 1999``` ou ano de preferência.

Método PUT: 
```"Id": 4``` id que deseja-se atualizar.;
```"Marca": "FIAT"``` ou marca de preferência;
```"AnoFabricacao": 1999``` ou ano de preferência.

Método DELETE: 
Na URL digite ```localhost:5067/carros?id=1``` o número depois do igual deve ser substituído pelo id do carro que é esperado ser deletado.

📄 Licença

Este é um projeto acadêmico sem fins comerciais.
