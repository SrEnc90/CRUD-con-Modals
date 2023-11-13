
--BULK INSERT (Insertamos datos de manera masiva)
/*
Creamos una tabla intermedia llamada dbo.tablaTemporal
Creamos columnas en dicha tabla de tipo varchar(max). La diferencia entre varchar y nvarchar es que nvarchar acepta
valores de tipo unicode(cómo por ejemplo abecedario chino) pero también ocupa más espacio
*/

BULK INSERT [dbo].[tablaTemporal] FROM 'C:\WorkSpace\C#\Sol_NeoCrudModal\DataMexico\CPdescarga.txt'
WITH (
	FIELDTERMINATOR = '|',
	ROWTERMINATOR = '\n',
	FIRSTROW = 3, --Que nuestra información comienza desde la línea 3
	CODEPAGE = '1252' --Código de Página, nos permite configurar el idioma de archivo: 1252 = Es para idioma español latino, acepta acentos y ñ's
)

--Ejemplo de tabla Temporal
CREATE TABLE #temporal (
	nombre varchar(30),
	clave varchar(30)
)

INSERT INTO #temporal VALUES ('Carlos', '1234'), ('Cielo', '4321'), ('Evelyn', '1991')


