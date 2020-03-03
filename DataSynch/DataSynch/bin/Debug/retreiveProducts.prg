
	SELECT 0
	USE fp SHARED ALIAS fpTemp
	SELECT * FROM fpTemp INTO CURSOR fpCursor WHERE !DELETED()
	SELECT * FROM fpTemp INTO CURSOR fpDeleted WHERE DELETED()
	USE IN fpTemp
	SELECT fpCursor
	handler=FCREATE('produse.sql')
	SCAN
		 insertString = "INSERT INTO produse(local_id,codp,denm,um,tva,pc,pcv,contp,pv,grupa,fuben,producator,pcval,pvval,codval,data,um2,um1um2,"+;
						"um3,um2um3,incadrare,ct,net,reteta_old,formula,dataexp,lot,codtimbru,divizie,psmentor,codext,denext,"+;
						"cod394,pcrol,pvrol,pcvrol,z_old) "+;
						"VALUES("+formatID(fpCursor.id)+;
							",'"+ALLTRIM(fpCursor.codp)+"'"+;
							",'"+dataFormat(fpCursor.denm)+"'"+;
							",'"+dataFormat(fpCursor.um)+"'"+;
							","+ALLTRIM(STR(fpCursor.tva,2,0))+;
							","+ALLTRIM(STR(fpCursor.pc,20,8))+;
							","+ALLTRIM(STR(fpCursor.pcv,20,8))+;
							",'"+dataFormat(fpCursor.contp)+"'"+;
							","+ALLTRIM(STR(fpCursor.pv,20,8))+;
							",'"+dataFormat(fpCursor.grupa)+"'"+;
							",'"+dataFormat(fpCursor.fuben)+"'"+;
							",'"+dataFormat(fpCursor.producator)+"'"+;
							","+ALLTRIM(STR(fpCursor.pcval,20,8))+;
							","+ALLTRIM(STR(fpCursor.pvval,20,8))+;
							",'"+dataFormat(fpCursor.codval)+"'"+;
							",'"+dataFormat(fpCursor.data)+"'"+;
							",'"+dataFormat(fpCursor.um2)+"'"+;
							","+ALLTRIM(STR(fpCursor.um1um2,10,2))+;
							",'"+dataFormat(fpCursor.um3)+"'"+;
							","+ALLTRIM(STR(fpCursor.um2um3,10,2))+;
							",'"+dataFormat(fpCursor.incadrare)+"'"+;
							","+ALLTRIM(STR(fpCursor.ct,11,4))+;
							","+ALLTRIM(STR(fpCursor.net,11,4))+;
							","+dataFormat(fpCursor.reteta)+;
							",'"+dataFormat(fpCursor.formula)+"'"+;
							",'"+dataFormat(fpCursor.dataexp)+"'"+;
							",'"+dataFormat(fpCursor.lot)+"'"+;
							",'"+dataFormat(fpCursor.codtimbru)+"'"+;
							",'"+dataFormat(fpCursor.divizie)+"'"+;
							","+ALLTRIM(STR(fpCursor.psmentor,10,0))+;
							",'"+dataFormat(fpCursor.codext)+"'"+;
							",'"+dataFormat(fpCursor.denext)+"'"+;
							",'"+dataFormat(fpCursor.cod394)+"'"+;
							","+ALLTRIM(STR(fpCursor.pcrol,20,8))+;
							","+ALLTRIM(STR(fpCursor.pvrol,20,8))+;
							","+ALLTRIM(STR(fpCursor.pcvrol,20,8))+""+;
							","+dataFormat(fpCursor.z_)+")"+CHR(13)+;
							"ON CONFLICT (local_id) DO UPDATE SET "+;
							"codp = "+"'"+ALLTRIM(fpCursor.codp)+"'"+;
							",denm = "+"'"+dataFormat(fpCursor.denm)+"'"+;
							",um = "+"'"+dataFormat(fpCursor.um)+"'"+;
							",tva = "+ALLTRIM(STR(fpCursor.tva,2,0))+;
							",pc = "+ALLTRIM(STR(fpCursor.pc,20,8))+;
							",pcv = "+ALLTRIM(STR(fpCursor.pcv,20,8))+;
							",contp = "+"'"+dataFormat(fpCursor.contp)+"'"+;
							",pv = "+ALLTRIM(STR(fpCursor.pv,20,8))+;
							",grupa = "+"'"+dataFormat(fpCursor.grupa)+"'"+;
							",fuben = "+"'"+dataFormat(fpCursor.fuben)+"'"+;
							",producator = "+"'"+dataFormat(fpCursor.producator)+"'"+;
							",pcval = "+ALLTRIM(STR(fpCursor.pcval,20,8))+;
							",pvval ="+ALLTRIM(STR(fpCursor.pvval,20,8))+;
							",codval = "+"'"+dataFormat(fpCursor.codval)+"'"+;
							",data = "+"'"+dataFormat(fpCursor.data)+"'"+;
							",um2 = "+"'"+dataFormat(fpCursor.um2)+"'"+;
							",um1um2 = "+ALLTRIM(STR(fpCursor.um1um2,10,2))+;
							",um3 = "+"'"+dataFormat(fpCursor.um3)+"'"+;
							",um2um3 = "+ALLTRIM(STR(fpCursor.um2um3,10,2))+;
							",incadrare = "+"'"+dataFormat(fpCursor.incadrare)+"'"+;
							",ct = "+ALLTRIM(STR(fpCursor.ct,11,4))+;
							",net = "+ALLTRIM(STR(fpCursor.net,11,4))+;
							",reteta_old = "+dataFormat(fpCursor.reteta)+;
							",formula = "+"'"+dataFormat(fpCursor.formula)+"'"+;
							",dataexp = "+"'"+dataFormat(fpCursor.dataexp)+"'"+;
							",lot = "+"'"+dataFormat(fpCursor.lot)+"'"+;
							",codtimbru ="+"'"+dataFormat(fpCursor.codtimbru)+"'"+;
							",divizie = "+"'"+dataFormat(fpCursor.divizie)+"'"+;
							",psmentor = "+ALLTRIM(STR(fpCursor.psmentor,10,0))+;
							",codext ="+"'"+dataFormat(fpCursor.codext)+"'"+;
							",denext = "+"'"+dataFormat(fpCursor.denext)+"'"+;
							",cod394 = "+"'"+dataFormat(fpCursor.cod394)+"'"+;
							",pcrol = "+ALLTRIM(STR(fpCursor.pcrol,20,8))+;
							",pvrol ="+ALLTRIM(STR(fpCursor.pvrol,20,8))+;
							",pcvrol = "+""+ALLTRIM(STR(fpCursor.pcvrol,20,8))+""+;
							",z_old = "+dataFormat(fpCursor.z_)+";"
		=fputs(handler,insertString)
	ENDSCAN
	SELECT fpDeleted
	SCAN
		insertString = "UPDATE produse SET deleted = true WHERE local_id = "+formatID(fpCursor.id)+";"
		=fputs(handler,insertString)
	ENDSCAN
	=fclose(handler)
***return is needed to exit the program
RETURN 
*************************************************
PROCEDURE dataFormat
PARAMETERS value
	IF VARTYPE(value)=="D"
		IF(EMPTY(value)) 
			RETURN '2000-01-01'
		ELSE
			newDate = PADL(YEAR(value),4,'0')+'-'+PADL(MONTH(value),2,'0')+'-'+PADL(DAY(value),2,'0')
			RETURN newDate
		ENDIF
	ENDIF
	IF VARTYPE(value)=="C"
		RETURN ALLTRIM(STRTRAN(STRTRAN(value,"'",""),'"',''))
	ENDIF
	IF VARTYPE(value)=="L"
		RETURN IIF(value,"true","false")
	ENDIF
RETURN
*************************************************
**For now this procedure does nothing <= needs to be used after finishing testing
PROCEDURE formatID
PARAMETERS value
	localID = ALLTRIM(STR(value,20))
	localIDPrefix = '1'
	return ALLTRIM(localIDPrefix + localID)
RETURN
