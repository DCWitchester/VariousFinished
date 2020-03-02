
	SELECT 0
	USE fp SHARED ALIAS fpTemp
	SELECT * FROM fpTemp INTO CURSOR fpCursor WHERE !DELETED()
	USE IN fpTemp
	SELECT fpCursor
	handler=FCREATE('produse.sql')
	SCAN
		insertString = "INSERT INTO produse(local_id,codp,denm,um,tva,pc,pcv,contp,pv,grupa,fuben,producator,pcval,pvval,codval,data,um2,um1um2,"+;
						"um3,um2um3,incadrare,ct,net,reteta_old,formula,dataexp,lot,codtimbru,divizie,psmentor,divizie,psmentor,codext,denext,"+;
						"cod394,pcrol,pvrol,pcvrol,z_old) "+;
						"VALUES("+ALLTRIM(STR(fpCursor.id,20))+;
							",'"+ALLTRIM(fpCursor.codp)+"'"+;
							",'"+ALLTRIM(fpCursor.denm)+"'"+;
							",'"+ALLTRIM(fpCursor.um)+"'"+;
							","+ALLTRIM(STR(fpCursor.tva,2,0))+;
							","+ALLTRIM(STR(fpCursor.pc,20,8))+;
							","+ALLTRIM(STR(fpCursor.pcv,20,8))+;
							",'"+ALLTRIM(fpCursor.contp)+"'"+;
							","+ALLTRIM(STR(fpCursor.pv,20,8))+;
							",'"+ALLTRIM(fpCursor.grupa)+"'"+;
							",'"+ALLTRIM(fpCursor.fuben)+"'"+;
							",'"+ALLTRIM(fpCursor.producator)+"'"+;
							","+ALLTRIM(STR(fpCursor.pcval,20,8))+;
							","+ALLTRIM(STR(fpCursor.pvval,20,8))+;
							",'"+ALLTRIM(fpCursor.codval)+"'"+;
							",'"+dateFormat(fpCursor.data)+"'"+;
							",'"+ALLTRIM(fpCursor.um2)+"'"+;
							","+ALLTRIM(STR(fpCursor.um1um2,10,2))+;
							",'"+ALLTRIM(fpCursor.um3)+"'"+;
							","+ALLTRIM(STR(fpCursor.um2um3,10,2))+;
							",'"+ALLTRIM(fpCursor.incadrare)+"'"+;
							","+ALLTRIM(STR(fpCursor.ct,11,4))+;
							","+ALLTRIM(STR(fpCursor.net,11,4))+;
							","+IIF(fpCursor.reteta,"true","false")+;
							",'"+ALLTRIM(fpCursor.formula)+"'"+;
							",'"+dateFormat(fpCursor.dataexp)+"'"+;
							",'"+ALLTRIM(fpCursor.lot)+"'"+;
							",'"+ALLTRIM(fpCursor.codtimbru)+"'"+;
							",'"+ALLTRIM(fpCursor.divizie)+"'"+;
							","+ALLTRIM(STR(fpCursor.psmentor,10,0))+;
							",'"+ALLTRIM(fpCursor.codext)+"'"+;
							",'"+ALLTRIM(fpCursor.denext)+"'"+;
							",'"+ALLTRIM(fpCursor.cod394)+"'"+;
							","+ALLTRIM(STR(fpCursor.pcrol,20,8))+;
							","+ALLTRIM(STR(fpCursor.pvrol,20,8))+;
							",'"+ALLTRIM(STR(fpCursor.pcvrol,20,8))+"'"+;
							","+IIF(fpCursor.z_,"true","false")+")"
		=fputs(handler,insertString)
	ENDSCAN
	=fclose(handler)
***return is needed to exit the program
RETURN 
*************************************************
PROCEDURE dateFormat
PARAMETERS value
	IF(EMPTY(value)) 
		RETURN '2000-01-01'
	ELSE
		newDate = PADL(YEAR(value),4,'0')+'-'+PADL(MONTH(value),2,'0')+'-'+PADL(DAY(value),2,'0')
		RETURN newDate
	ENDIF
*************************************************
