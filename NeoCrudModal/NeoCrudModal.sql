
--BULK INSERT (Insertamos datos de manera masiva)
/*
Creamos una tabla intermedia llamada dbo.tablaTemporal
Creamos columnas en dicha tabla de tipo varchar(max). La diferencia entre varchar y nvarchar es que nvarchar acepta
valores de tipo unicode(c�mo por ejemplo abecedario chino) pero tambi�n ocupa m�s espacio
*/

BULK INSERT [dbo].[tablaTemporal] FROM 'C:\WorkSpace\C#\Sol_NeoCrudModal\DataMexico\CPdescarga.txt'
WITH (
	FIELDTERMINATOR = '|',
	ROWTERMINATOR = '\n',
	FIRSTROW = 3, --Que nuestra informaci�n comienza desde la l�nea 3
	CODEPAGE = '1252' --C�digo de P�gina, nos permite configurar el idioma de archivo: 1252 = Es para idioma espa�ol latino, acepta acentos y �'s
)

--Ejemplo de tabla Temporal
CREATE TABLE #temporal (
	nombre varchar(30),
	clave varchar(30)
)

INSERT INTO #temporal VALUES ('Carlos', '1234'), ('Cielo', '4321'), ('Evelyn', '1991')


