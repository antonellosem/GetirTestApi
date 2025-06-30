@echo off

set Home=%CD%
set folder="PgadService"

:Brand
#set /P brand=Quale brand?(I)ntralot, (B)etsson, (M)ondogoal , (G)oldbet
set /P brand=Quale brand?(B)etsson, (M)ondogoal , (G)oldbet
IF /I "%brand%" =="I" GOTO scegliIntralot
IF /I "%brand%" =="M" GOTO scegliMondogoal
IF /I "%brand%" =="B" GOTO scegliBetsson
IF /I "%brand%" =="G" GOTO scegliGoldbet
echo scelta non valida.
goto Brand


:scegliIntralot
set /P ambiente=Quale ambiente vuoi configurare?(S)viluppo, (C)ollaudo, (Ce)rtificazione, (P)roduzione: 
IF /I "%ambiente%" =="S" GOTO sviluppointralot
IF /I "%ambiente%" =="C" GOTO collaudointralot
IF /I "%ambiente%" =="CE" GOTO certificazioneintralot
IF /I "%ambiente%" =="P" GOTO produzioneintralot
echo scelta non valida.
goto scegliIntralot

:scegliGoldbet
set /P ambiente=Quale ambiente vuoi configurare?(S)viluppo, (C)ollaudo, (N)ewStaging, (Q)a, (P)roduzione: 
IF /I "%ambiente%" =="S" GOTO sviluppogoldbet
IF /I "%ambiente%" =="C" GOTO collaudogoldbet
IF /I "%ambiente%" =="N" GOTO newstaginggoldbet
IF /I "%ambiente%" =="Q" GOTO qagoldbet
IF /I "%ambiente%" =="P" GOTO produzionegoldbet
echo scelta non valida.
goto scegliGoldbet


:scegliMondogoal
set /P ambiente=Quale ambiente vuoi configurare?(D)ev, (P)roduzione: 
IF /I "%ambiente%" =="D" GOTO sviluppomondogoal
IF /I "%ambiente%" =="P" GOTO produzionemondogoal
echo scelta non valida.
goto scegliMondogoal

:scegliBetsson
set /P ambiente=Quale ambiente vuoi configurare?(C)ollaudo, (P)roduzione: 
IF /I "%ambiente%" =="C" GOTO collaudobetsson
IF /I "%ambiente%" =="P" GOTO produzionebetsson
echo scelta non valida.
goto scegliBetsson

:sviluppointralot
set appsettings=Intralot.Sviluppo.AppSettings.config
set connectionstrings=Intralot.Sviluppo.ConnectionStrings.config
set webconfig=Intralot.Sviluppo.web.config
GOTO configura

:collaudointralot
set appsettings=Intralot.collaudo.AppSettings.config
set connectionstrings=Intralot.collaudo.ConnectionStrings.config
set webconfig=Intralot.collaudo.web.config
GOTO configura

:certificazioneintralot
set appsettings=Intralot.certificazione.AppSettings.config
set connectionstrings=Intralot.certificazione.ConnectionStrings.config
set webconfig=Intralot.certificazione.web.config
GOTO configura

:produzioneintralot
set appsettings=Intralot.produzione.AppSettings.config
::set connectionstrings=Intralot.produzione.ConnectionStrings.config
set connectionstrings="Intralot.Produzione.ConnectionStrings_ONLINE.config"
set webconfig=Intralot.produzione.web.config
GOTO configura

:sviluppogoldbet
set appsettings=Goldbet.Sviluppo.AppSettings.config
set connectionstrings=Goldbet.Sviluppo.ConnectionStrings.config
set webconfig=Goldbet.Sviluppo.web.config
set nlog=Goldbet.Sviluppo.nlog.config
GOTO configura

:collaudogoldbet
set appsettings=Goldbet.Collaudo.AppSettings.config
set connectionstrings=Goldbet.Collaudo.ConnectionStrings.config
set webconfig=Goldbet.Collaudo.web.config
set nlog=Goldbet.Collaudo.nlog.config
GOTO configura

:newstaginggoldbet
set appsettings=Goldbet.NewStaging.AppSettings.config
set connectionstrings=Goldbet.NewStaging.ConnectionStrings.config
set webconfig=Goldbet.NewStaging.web.config
set nlog=Goldbet.NewStaging.nlog.config
GOTO configura

:qagoldbet
set appsettings=Goldbet.QA.AppSettings.config
set connectionstrings=Goldbet.QA.ConnectionStrings.config
set webconfig=Goldbet.QA.Web.config
set nlog=Goldbet.QA.nlog.config
GOTO configura

:produzionegoldbet
set appsettings=Goldbet.Produzione.AppSettings.config
::set connectionstrings=Goldbet.produzione.ConnectionStrings.config
set connectionstrings="Goldbet.Produzione.ConnectionStrings.config"
set webconfig=Goldbet.Produzione.web.config
set nlog=Goldbet.Produzione.nlog.config
GOTO configura

:sviluppomondogoal
set appsettings=Mondogoal.Dev.AppSettings.config
set connectionstrings=Mondogoal.Dev.ConnectionStrings.config
set webconfig=Mondogoal.Dev.web.config
GOTO configura

:produzionemondogoal
set appsettings=Mondogoal.produzione.AppSettings.config
::set connectionstrings=Mondogoal.produzione.ConnectionStrings.config
set connectionstrings="Mondogoal.Produzione.ConnectionStrings_ONLINE.config"
set webconfig=Mondogoal.produzione.web.config
GOTO configura

:sviluppobetsson
set appsettings=Betsson.Sviluppo.AppSettings.config
set connectionstrings=Betsson.Sviluppo.ConnectionStrings.config
set webconfig=Betsson.Sviluppo.web.config
GOTO configura

:collaudobetsson
set appsettings=Betsson.Collaudo.AppSettings.Config
set connectionstrings=Betsson.Collaudo.ConnectionStrings.Config
set webconfig=Betsson.Collaudo.Web.Config
set nlog=Betsson.Collaudo.nlog.config
GOTO configura

:produzionebetsson
set appsettings=Betsson.produzione.AppSettings.config
set connectionstrings=Betsson.produzione.ConnectionStrings.config
set webconfig=Betsson.produzione.web.config
set nlog=Betsson.produzione.nlog.config
GOTO configura


:configura
cd "%folder%/WebConfig"
copy /Y %appsettings% AppSettings.config 
copy /Y %connectionstrings% ConnectionStrings.config 
copy /Y %webconfig% ..\web.config 
copy /Y %nlog% ..\nlog.config 
cd %home%
goto eof

:eof
pause Premi un tasto per chiudere