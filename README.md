# CRUD AdoNet

Esse é um exemplo de implementação de CRUD (Create, Read, Update e Delete) usando AdoNet com C#.

## Funcionamento

O menu principal apresenta as seguintes opções ao usuário:

1. Listar todos os Clientes
2. Alterar os dados de um Cliente
3. Inserir um novo cliente
4. Deletar um cliente
5. Limpar o Chat
6. Encerrar programa

O usuário seleciona uma dessas opções e a ação correspondente é realizada.

## Configuração

A string de conexão com o banco de dados é hardcoded temporariamente no método `StringConnection()`. Substitua os valores de `SeuServidorAqui`, `PortaDoServidor`, `BancaDeDadosAqui`, `SeuUsuarioAqui` e `SuaSenhaAqui` com as informações do seu banco de dados.

## Considerações Finais

Este é apenas um exemplo básico de como implementar um CRUD usando AdoNet. É importante seguir boas práticas de segurança e otimização em aplicações reais.
