
## Desafio C# - Atos Capital

Este é um projeto simples que implementa um algoritmo de busca em profundidade para solucionar um labirinto representado por uma matriz de strings,construída utilizando a seguinte tecnologia:

O código fonte está em C# e foi desenvolvido usando a plataforma .NET Core. O programa recebe como entrada um arquivo de texto que representa a matriz do labirinto e a posição de início e de saída. Em seguida, o programa procura um caminho através do labirinto e exibe o trajeto na tela.

Para executar o programa, é necessário ter o .NET Core instalado no computador. Basta abrir o prompt de comando ou o terminal na pasta do projeto e digitar o comando "dotnet run". Em seguida, informe o caminho do arquivo de entrada quando solicitado.

Exemplo de arquivo de entrada :
[Entrada](https://github.com/igorsalgado/Desafio-AtosCapital/blob/master/entrada-labirinto.txt)

Onde
- 1 indica uma parede
- 0 indica um caminho possível de se trafegar
- X é o ponto de partida

Exemplo de arquivo de saída :
[Saída](https://github.com/igorsalgado/Desafio-AtosCapital/blob/master/saida-entrada-labirinto.txt)


Onde 
 - C (Ir para cima)
 - E (Ir para Esquerda)
 - D (Ir para Direita)
 - B (Ir para Baixo)


## Desafio

O desafio é consite em elaborar código-fonte que seja capaz de:

- Ler o arquivo texto de entrada

- Identificar a dimensão da matriz do labirinto, em que o primeiro número indica o número de linhas e o segundo número indica o número de colunas (é separado por espaço).

- Identificar a posição de origem (ponto O localizado dentro da matriz). A posição “aumenta de valor” lendo de cima para baixo e/ou da esquerda para a direita. A posição na extremidade superior esquerda é a [1, 1] (linha 1 coluna 1) e a posição na extremidade inferior direita é a que representa o número de linhas e o número de colunas [L, C] (exemplo, se tem 4 linhas e 5 colunas, esta extremidade em questão é a [4, 5]).

-  A partir do ponto de origem se deslocar (seguindo a ordem de prioridade de deslocamento) e encontrar a única saída (que se encontra no ponto 0 localizado em uma extremidade da matriz)

- Ao encontrar a saída gerar um arquivo texto de saída (na mesma pasta onde está o arquivo de entrada, só que com outro nome de arquivo. 

- Ex: entrada.txt é arquivo de entrada então o arquivo de saída pode ser saída-entrada.txt) contendo cada passo do trajeto, onde cada linha indica a direção e posição destinada. A primeira linha do arquivo de saída deve estar com O (origem) seguido da posição inicial.
