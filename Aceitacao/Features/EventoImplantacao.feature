@EventoImplantacao
Funcionalidade: Evento de Implantação	

@ignore
Cenário: Implantacao de uma proposta
	Dado que há uma proposta com os seguintes dados
	| Identificador                        | IdExterno          | ItemProdutoId | DataInicioVigencia | ClasseRiscoId | TipoFormaContratacaoId | TipoRendaId | IdentificadorNegocio | InscricaoId  | Matricula | Periodicidade | DataNascimento | Sexo      |
	| 9d2b0228-4d0d-4c23-8b49-01a698857709 | 10116682815341371  | 153417        | 01/01/2016         | 10            | 5                      | 2           | 245a444f-fc39-4ee1   | 9994447091   | 631011    | 30            | 01/01/1980     | Masculino |
	Quando processar o evento de implantacao
	Então deve ser criado um evento implantado com os seguintes dados
	| Identificador                        | IdentificadorNegocio |
	| 9d2b0228-4d0d-4c23-8b49-01a698857709 | 245a444f-fc39-4ee1   |
	E uma cobertura contratada deve conter
	| IdExterno                | ItemProdutoId | DataInicioVigencia | ClasseRiscoId | TipoFormaContratacaoId | TipoRendaId | InscricaoId | Matricula |
	| 10116682815341371        | 153417        | 01/01/2016         | 10            | 5                      | 2           | 9994447091  | 631011    |
	E um historico de cobertura com
	| Periodicidade | DataNascimento | Sexo      |
	| Mensal        | 01/01/1980     | Masculino |


@ignore	
Cenário: Implantacao duas proposta com o mesmo identificador
	Dado que há uma implantacao com
	| Identificador                        | IdExterno          | ItemProdutoId | DataInicioVigencia | ClasseRiscoId | TipoFormaContratacaoId | TipoRendaId | IdentificadorNegocio | InscricaoId  | Matricula | Periodicidade | DataNascimento | Sexo      |
	| 9d2b0228-4d0d-4c23-8b49-01a698857709 | 10116682815341371  | 153417        | 01/01/2016         | 0             | 1                      | 1           | 245a444f-fc39-4ee1   | 101166828534 | 100       | 30            | 01/01/1980     | Masculino |
	E uma outra implantacao com o mesmo IdExterno
	| Identificador                        | IdExterno          | ItemProdutoId | DataInicioVigencia | ClasseRiscoId | TipoFormaContratacaoId | TipoRendaId | IdentificadorNegocio | InscricaoId  | Matricula | Periodicidade | DataNascimento | Sexo      |
	| 9d2b0228-4d0d-4c23-8b49-01a698857709 | 10116682815341371  | 153417        | 01/01/2016         | 0             | 1                      | 1           | 245a444f-fc39-4ee1   | 101166828534 | 100       | 30            | 01/01/1980     | Masculino |
	Quando Processar os evento
	Então deve criar apenas um evento com uma cobertura
	

@ignore
Cenário: Implantacao de uma proposta com cobertura existente
	Dado que há uma cobertura com os seguintes dados
	| Identificador                        | IdExterno          | ItemProdutoId | DataInicioVigencia | ClasseRiscoId | TipoFormaContratacaoId | TipoRendaId | IdentificadorNegocio | InscricaoId  | Matricula | Periodicidade | DataNascimento | Sexo      |
	| 9d2b0228-4d0d-4c23-8b49-01a698857709 | 10116682815341371  | 153417        | 01/01/2016         | 0             | 1                      | 1           | 245a444f-fc39-4ee1   | 101166828534 | 100       | 30            | 01/01/1980     | Masculino |
	Quando Processar o evento
	Então ocorre um erro: "O identificador externo 10116682815341371 já foi implantado anteriormente."