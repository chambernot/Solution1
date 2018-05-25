@EventoAlteracaoParametros
Funcionalidade: Evento de Altereção de Parametros	

@igonre
Cenário: Alteração de Parametros
	Dado que há um evento de atualizacao de parametros com os seguintes dados
	| Identificador                        | IdExterno          | ItemProdutoId | DataInicioVigencia | ClasseRiscoId | TipoFormaContratacaoId | TipoRendaId | IdentificadorNegocio | InscricaoId  | Matricula | Periodicidade | DataNascimento | Sexo      |
	| 9B2B0228-4D0D-4C23-8B49-01A698857709 | 10116682815341371  | 153417        | 01/01/2016         | 0             | 1                      | 1           | 245a444f-fc39-4ee1   | 101166828534 | 100       | 30            | 01/01/1980     | Masculino |
	Quando processar o evento
	Então deve ser criado um evento com os dados abaixo
	| Identificador                        | IdentificadorNegocio |
	| 9B2B0228-4D0D-4C23-8B49-01A698857709 | 245a444f-fc39-4ee1   |
	E um historico de cobertura contratada com
	| Periodicidade | DataNascimento | Sexo      |
	| 30	        | 01/01/1980     | Masculino |
