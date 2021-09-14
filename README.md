## DESAFIO TÉCNICO

Projeto desenvolvido para demonstração de conhecimentos de criação e integração de APIs na plataforma .net

## Requisitos

Os requisitos foram disponibilizados no seguint [link](https://drive.google.com/file/d/15NBGbmQO7FMnJDbR8Mm_QrJDSyw-5E5s/view)

## Informações para execução:

1. Clone este repositório 
2. Restaure os pacotes utilizando .net5 ou superior
3. Configure o endereço que será executado o projeto TaxaJurosService, no appsettings de CalculoJurosService (default será https://localhost:44300/)
4. Execute ambos os projetos CalculoJurosService e TaxaJurosService simultaneamente

## Extras

1. :x: Utilização de Docker
2. :heavy_check_mark: Teste de integração da API em linguagem de sua preferência (Damos
importância para pirâmide de testes)
3. :heavy_check_mark: Utilizar swagger

## Informações do projeto

* Ambas as APIs foram desenvolvidas em .net 5, o mesmo vale para os projetos de teste
* A solução foi dividida em camadas de src e tests para facilitar o entendimento das responsabilidades (inspiradas no DDD de E.Evans e [CA](https://github.com/jasontaylordev/CleanArchitecture) de J.Taylor)
* Os fontes dentro de src são subdivididos entre 
  * Infraestructure = Camada que é responsável por configurar e gerenciar as dependências externas do projeto e onde a implementação de interfaces com recursos externos ([EF](https://docs.microsoft.com/pt-br/ef/core/) ou [Dapper](https://github.com/DapperLib/Dapper) para um ou mais bancos de dados, serviços de email ou I/O). Como nesse projeto não foram necessários tais recursos nessa camada vemos a configuração de recursos como [Refit](https://github.com/reactiveui/refit), Swagger e tratamento de exceções genéricas.
  * Application = Camada que define as implementações de regra de negócio e define os contratos de recursos externos. A regra do cálculo de juros está nessa camada, além da definição da interface de comunicação utilizada pelo [Refit](https://github.com/reactiveui/refit) para a comunicação com a API de taxa de juros.
  * Presentation = Camada que registra as 2 APIs requisitadas para o desafio:
    * TaxaJurosService: Foi considerada a utilização de [minimal API](https://www.youtube.com/watch?v=enAskgcF0c0&ab_channel=dotNET) pelo ganho de performance e simplicidade dos requisitos da aplicação, contudo como a funcionalidade ainda está em preview na versão 6 do dotnet e para facilidade na execução do projeto pelo avaliador (sem precisar baixar um runtime em preview), foi optado pela implementação clássica. A Api possui apenas o endpoint solicitado "/taxaJuros" que retorna a taxa de juros configurada no appsettings (0,01). Esse projeto em específico e totalmente independente pois não vi necessidade de dividir um escopo tão pequeno em diversos projetos.
    * CalculoJurosService: Neste projeto, foram implementados os seguintes endpoints:
      1. "/showmethecode" =  Controller simples que retorna a Uri configurada na propriedade "GithubRepo" do appsettings, no caso a uri desse repositório.
      2. "/calculaJuros" = Esse recurso faz chamada a camada de application para execução da regra de negócio e utiliza o [Refit](https://github.com/reactiveui/refit) configurado na camada de infrastructure para realizar a consulta da taxa de juros na API TaxaJurosService (o endereço deve ser informado em "TaxaJurosServiceConfig:BaseURl" no appsettings). Tudo isso para fazer o cálculo dos juros utilizando os parâmetros recebidos via url.
* Os fontes na pasta tests foram divididos entre 3 projetos
  * Application.Unit.Tests: Responsável pelos testes de unidade da camada de aplicação. Os testes foram implementados utilizando [xUnit](https://xunit.net/) e [Moq](https://github.com/Moq/moq4/wiki/Quickstart)
  * CalculoJurosService.Integration.Tests e TaxaJurosService.Integration.Tests: Ambos contêm os testes de integração referentes a cada API referenciada. Os testes utilizaram [xUnit](https://xunit.net/) e [Wiremock](https://github.com/WireMock-Net/WireMock.Net)  
  * o [Github actions](https://github.com/weldermarcosxd/juros-api/actions) pode ser utilizado para validação das builds   


