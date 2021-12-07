# I. Código
# II. Pedro Passos 22002182


# Projeto 1 de Linguagens de Programação 2 | 2021/2022

## Autoria

- José Reis - 22002053
  - Planeamento da estrutura do código
  - Elaboração de métodos para as diversas classes
  - Manutenção da documentação

- Pedro Ferreira - 220006045
  - Elaboração da abertura dos ficheiros
  - Elaboração de métodos para as diversas classes
  - Revisão do código e do relatório

- Pedro Passos - 22002182
  - Elaboração do visual no unity
  - Elaboração de métodos para as diversas classes
  - Elaboração do relatório

É importante realçar que de forma geral todos fizeram no mínimo um pouco de tudo, pois a maioria do trabalho foi feita em chamada e discutido em grupo.

## Legendas

### Legenda dos Tiles

        🟦 - Ocean
        🟩 - Grassland
        🟨 - Dessert
        🟫 - Mountain
        ⬜ - Hills

### Legenda dos Recursos

        🟩  - Plants
        🟥 - Animals
        🟦 - Luxury
        ⬜ - Metals
        ⬛ - Pollution
        🟧 - FossilFuel

## Arquitetura da Solução

### Descrição da Solução

Para a elaboração deste projeto foi utilizado o _design pattern Model-View-Controller_
(MVC). O modelo é constituído pela classe `Map`. A _view_ é constituída pelo _UI_
da aplicação e pelas classes `PopulateGrid` e `ErrorDisplayView`. O controlador
é constituído pela classe `Controller`.

Ao iniciar a aplicação é apresentado o menu inicial, no qual o utilizador pode
escolher entre carregar um ficheiro, visualizar as instruções de utilização ou fechar
a aplicação.

Ao selecionar a opção de carregar um ficheiro é apresentada uma janela na qual o
O utilizador pode procurar pelo ficheiro que deseja carregar. Esta janela apenas
permite que sejam selecionados ficheiros com a extensão .map4x, pelo que não
apresenta qualquer outro ficheiro como opção de escolha.  
É de notar que são apresentadas pastas, nas quais o utilizador pode entrar para
procurar o ficheiro, no entanto não é possível selecionar uma pasta como ficheiro
para carregar.

Este modo de seleção de ficheiro foi implementado através da utilização do _asset
UnitySimpleFileBrowser_, referido no enunciado do projeto.

Após selecionado o ficheiro, começa o processo de análise do mesmo. O controlador
analisa o ficheiro linha a linha, sendo que caso seja encontrado algum tipo de
erro, é apresentada uma mensagem de erro.

Ao encontrar um erro no ficheiro, o controlador invoca a função `OnErrorFound()`,
que faz com que os _listeners_ do evento `ErrorFound` reajam ao mesmo. Neste caso,
apenas a função `DisplayErrorMessage()` é chamada, apresentando uma mensagem de
erro, com base no `ErrorCode` passado como um argumento.

Esta função está presente na classe `ErrorDisplayView`, cuja única responsabilidade
é apresentar mensagens de erro ao utilizador. Este aspeto de uma classe ter apenas
uma responsabilidade foi aplicado ao máximo a todas as outras classes, de modo a
seguir os princípios _SOLID_.

Mesmo que um erro seja encontrado, após ser apresentada a mensagem de erro, o
utilizador pode tentar selecionar outro ficheiro, sem necessitar de fechar e
voltar a abrir a aplicação.

Caso não seja encontrado nenhum erro ao ler o ficheiro, é carregada uma nova
_scene_ na qual é apresentado o mapa criado com base nas informações presentes
no ficheiro.

A apresentação do mapa cabe à classe `PopulateGrid`, que através de um ciclo
percorre o mapa, obtendo a informação de cada _tile_ e ao mesmo tempo apresentando
uma representação visual desse mesmo _tile_ numa _layout grid_.

Esta _layout grid_ permite manter o mesmo tamanho para todos os _tiles_ e os
seus recursos, independentemente do tamanho do mapa criado. Caso o mapa não
caiba por completo no ecrã, é possível fazer _scroll_ na _layout grid_, podendo
ver o mapa na sua totalidade.

A cor de cada _tile_ depende do seu tipo de terreno. O mesmo se aplica para os
recursos presentes nos _tiles_. A cores que representam os terrenos e os recursos
podem ser observadas nas legendas apresentadas na seção anterior.

As possibilidades de tipo de terreno, tipo de recurso e _error codes_ foram
distribuídas pelas enumerações `TerrainType`, `ResourceType` e `ErrorCode`,
respetivamente.

Para se perceber qual o tipo de terreno e recursos a serem criados, foram criados
dicionários que contém os _key-value pairs_ das `strings` do ficheiro e o respetivo
terreno ou recurso. As _keys_ são as `strings` e os _pairs_ são o seu respectivo
`TerrainType` ou `ResourceType`.

O utilizador pode carregar em cima de cada _tile_ para obter informações mais
detalhadas acerca desse mesmo _tile_. Para a apresentação desta informação é ativado
um painel de um _canvas_, sendo este preenchido com a informação contida no _tile_
selecionado.

De modo a que a _view_ e o controlador partilhem a informação acerca do mesmo
mapa, foi utilizado o _ScriptableObject_ `MapContainer`, onde é guardada uma
referência a um mapa. Tanto o controlador como a _view_ possuem uma referência
ao mesmo _ScriptableObject_, partilhando assim a mesma informação acerca de um
mapa.

Após a apresentação do mapa, o jogador pode voltar ao menu inicial, podendo
selecionar um novo ficheiro, caso queira, rever as instruções de utilização ou
sair da aplicação.

### Diagrama UML

![DiagramaUML](img/umlDiagram.png "Diagrama UML")

## Referências

- [LP2 MVC Example](https://www.youtube.com/watch?v=_z_iRUjmvzE&t=4314s)
- [UnitySimpleFileBrowser](https://github.com/yasirkula/UnitySimpleFileBrowser)
- [UnitiSimpleFileBrowser Forum Thread](https://forum.unity.com/threads/simple-file-browser-open-source.441908/)
- [Unity UI Tutorial Grid Layout and Scroll Window](https://www.youtube.com/watch?v=VyIo5tlNNeA)
