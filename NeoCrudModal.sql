
--BULK INSERT (Insertamos datos de manera masiva)
BULK INSERT [dbo].[tablaTemporal] FROM 'C:\WorkSpace\C#\Sol_NeoCrudModal\DataMexico\CPdescarga.txt'
WITH (
	FIELDTERMINATOR = '|',
	ROWTERMINATOR = '\n',
	FIRSTROW = 3, --indica que la información comienza desde la línea 3
	/*La diferencia entre entre varchar y nvvarchar es que nvarchar acepta valores unicode (símbolos, letras en chino, etc.) pero ocupa más espacio en memoria*/
	CODEPAGE = '1252' --Código de Página, nos permite configurar el idioma de archivo: 1252 = Es para idioma español latino, acepta acentos y ñ's
)

TRUNCATE TABLE tablaTemporal -- Para eliminar la tabla temporal de manera más eficiente que hacer delete, ojo con cuidado

-- Ejemplo de tabla temporal. La tabla temporal no es permante ni se que crea en la base de datos(ver en el object explorer):
CREATE TABLE #temporal (
	usuario VARCHAR(30) primary key,
	contrasenia VARCHAR(10)
)

INSERT INTO #temporal VALUES ('Carlos', '1234'), ('Cielo', '4321'), ('Evelyn', '1991')

DROP TABLE #temporal --No sé si es necesario, me imagino que para liberar memoria

-- Expresiones Comunes de tabla o CTE(Common Table Expressions), es diferente a las tablas temporales(buscar diferencias y tiempo de vida)
--Te permite crear consultas temporales nombradas dentro de una consulta SELECT, INSERT, UPDATE o DELETE. Su tiempo de vida, es el de la consulta que has realizado
--Tabla Estado
WITH tempEstados AS (
	SELECT DISTINCT c_estado, d_estado FROM tablaTemporal
)
SELECT MAX(CAST(c_estado AS INT)), MAX(LEN(d_estado)) FROM tempEstados --Para saber el máxino length que debe almacenar cada columna y a partir de esto creamos la tabla

/*
TinyInt: de 0 a 255 registros
SmallInt: hasta 32,767 registros
Int: Más grande que SmallInt
BigInt:
*/
--Una vez creada la tabla, insertamos los registros de la tablaTemporal
WITH tempEstados AS (
	SELECT DISTINCT c_estado, d_estado FROM tablaTemporal
)
INSERT INTO [dbo].[Estados] (Id, Estado)
SELECT * FROM tempEstados --Parece que la sentencia no acaba hasta que haga un select a la tabla temporal

SELECT * FROM Estados --Recomendación: El nombre de las tablas siempre deben ir en mayúscula

SELECT top 10 * FROM tablaTemporal

--Tabla Municipio
--1ro Veo los campos que va a tener (c_mnpio no puede ser primary key xq se reinicia con cada nuevo c_estado, pero igual lo conservamos en la tabla Municipio (Columna Cve))
WITH tempMunicipio AS (
	SELECT c_estado, c_mnpio, D_mnpio FROM tablaTemporal
)
SELECT * FROM tempMunicipio
 --2do vemos la longitud máxima que deben tener y creamos la tabla (Ojo despues cambiamos a todos a int para no complicarnos con las restriciones pero si estamos seguro de que esos valores no van a hacer da mayor logintud lo podemos mantener)
WITH tempMunicipio AS (
	SELECT c_estado, c_mnpio, D_mnpio FROM tablaTemporal
)
SELECT MAX(CAST(c_estado AS INT)), MAX(CAST(c_mnpio AS INT)), MAX(LEN(D_mnpio)) FROM tempMunicipio
--3er insertamos en la Tabla Municipio
WITH tempMunicipio AS (
	SELECT DISTINCT c_estado, c_mnpio, D_mnpio FROM tablaTemporal
)
INSERT INTO Municipios ([IdEstado], [Cve], [Municipio])
SELECT * FROM tempMunicipio
--4to consultamos si la data está correcta
SELECT * FROM Municipios

--Tabla Asentamiento o también llamado CodigosPostales
--1ero
WITH tempAsentamiento AS (
	SELECT DISTINCT [Id], [d_codigo], [d_asenta], [c_estado], [c_mnpio] FROM tablaTemporal --Colocando el distinct se demora un poco más, investigar
	INNER JOIN [dbo].[Municipios] M ON  [IdEstado] = [c_estado] AND [Cve] = [c_mnpio]
)
SELECT * FROM tempAsentamiento ORDER BY 1

--2do
WITH tempAsentamiento AS (
	SELECT DISTINCT [Id], [d_codigo], [d_asenta], [c_estado], [c_mnpio] FROM tablaTemporal 
	INNER JOIN [dbo].[Municipios] M ON  [IdEstado] = [c_estado] AND [Cve] = [c_mnpio]
)
SELECT MAX(CAST([d_codigo] AS INT)), MAX(LEN([d_asenta])) FROM tempAsentamiento

--3ero
WITH tempAsentamiento AS (
	SELECT DISTINCT [Id], [d_codigo], [d_asenta], [c_estado], [c_mnpio] FROM tablaTemporal 
	INNER JOIN [dbo].[Municipios] M ON  [IdEstado] = [c_estado] AND [Cve] = [c_mnpio]
)
INSERT INTO [dbo].[CodigosPostales] ([IdMunicipio], [CodigoPostal], [Asentamiento]) -- El insert es con respecto a la línea de abajo
SELECT [Id], [d_codigo], [d_asenta] FROM tempAsentamiento ORDER BY 1

--4to
SELECT * FROM [dbo].[CodigosPostales]
