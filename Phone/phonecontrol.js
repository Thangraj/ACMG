
    var applethandle = null;
    var webphoneStatus = null;
    var theCallStatus = null;
    var theCallDetail = null;
    var thePhoneStatus = null;
    var theCDRCalled = null;
    var theCDRCaller = null;
    var theCDRPeer = null;
    var theCDRConnectTime = null;
    var theCDRDuration = null;
    var theCDRCallType = null;
    var theCDRDisconnectedBy = null;


    function trim(stringToTrim) {
        return stringToTrim.replace(/^\s+|\s+$/g, "");
    }


    function displaystatus(statustr) {
        if (webphoneStatus == null) {
            try { webphoneStatus = document.getElementById('webphoneStatus'); } catch (e) { }
        }
        try { webphoneStatus.innerHTML += statustr + '<br />'; } catch (e) { }
    }

    function displayCallstatus(statustr) {
        if (theCallStatus == null) {
            try { theCallStatus = document.getElementById('theCallStatus'); } catch (e) { }
        }
        try { theCallStatus.innerHTML = statustr + '<br />'; } catch (e) { }
    }



    
    function initcheck() {
        if (applethandle == null) {
            //displaystatus('JAVASCRIPT SAY: webphone init');

            try { applethandle = document.getElementById('webphone'); } catch (e) { }

            if (applethandle == null) {
                var applets = null;
                try {
                    applets = document.applets;

                    // Needed for FireFox
                    if (applets.length == 0) {
                        applets = document.getElementsByTagName("object");
                    }
                    if (applets.length == 0) {
                        applets = document.getElementsByTagName("applet");
                    }

                    //Find the active applet object
                    for (var i = 0; i < applets.length; ++i) {
                        try {
                            if (typeof (applets[i].API_Call) != "undefined") {
                                applethandle = applets[i];
                                break;
                            }
                        } catch (e) { }
                    }
                } catch (e) { }

                if (applethandle == null) try { applethandle = document.applets[0]; } catch (e) { }

                if (applethandle == null) {
                   // displaystatus('JAVASCRIPT SAY: cannot get applet handle');
                }
            }

            if (applethandle != null) {
                // See if we're using the old Java Plug-In and the JNLPAppletLauncher
                try {
                    applethandle = applethandle.getSubApplet();
                } catch (e) {
                    // Using new-style applet -- ignore
                }
            }
        }
    }

    /////////functions for web methods

    function fun() {
        PageMethods.GetPageMethod(onSucceed, onError);
        return false;
    }
    function onSucceed(result) {
        alert(result);
    }
    function onError(result) {
    }



     function saveCallDetail(CDRCalled,CDRCaller,CDRPeer,CDRConnectTime,CDRDuration,CDRCallType,CDRDisconnectedBy) {
         PageMethods.SaveCallDetail(CDRCalled, CDRCaller, CDRPeer, CDRConnectTime, CDRDuration, CDRCallType, CDRDisconnectedBy,onSucceedCDR, onErrorCDR);
         return false;
     }
     function onSucceedCDR(result) {
         alert(result);
     }
     function onErrorCDR(result) {
         alert(result);
     }

    /////////
   

     function checkPhoneStatus(theVar) {
         //split the value into an array
         var dataArray = theVar.split(",");
         
         //if this is a status event then update the status box
         if (dataArray[0].toString() == 'STATUS' && dataArray[1].toString() == '-1') {
             //uncommect next line to show status in a field on form
             //displayPhoneStatus(dataArray[2].toString());
             //if ready then register-replaced by parameters
             // if (dataArray[2].toString() == 'Ready.') {
             //voipAutoRegister(); 
             //  }
         }



         //if this is a call detail record then update the hidden fields and save the record
         if (dataArray[0].toString() == 'CDR') {
             displayCDRCalled(dataArray[4].toString());
             displayCDRCaller(dataArray[3].toString());
             displayCDRPeer(dataArray[2].toString());
             displayCDRConnectTime(dataArray[6].toString());
             displayCDRDuration(dataArray[7].toString());
             displayCDRDisconnectedBy(dataArray[8].toString());
             //displayPhoneStatus(dataArray[2].toString());
             if (dataArray[4] == dataArray[2]) { displayCDRCallType('outbound'); } else { displayCDRCallType('inbound'); }
             //raiseAsyncPostback();

             //call new web method
             saveCallDetail(dataArray[4].toString(), dataArray[3].toString(), dataArray[2].toString(), dataArray[6].toString(), dataArray[7].toString(),
              'outbound', dataArray[8].toString());

         }


         //txtCDRCalled 5
         //txtCDRCaller 4
         //txtCDRPeer 3
         //txtCDRConnectTime 7
         //txtCDRDuration 8
         //txtCDRCallType (if peer and called are the same then outbound - if peer and caller are same then inbound)
         //txtCDRDisconnectedBy 9
         //txtPhoneStatus

     }
    
    
    
    function webphonetojs(varr) {
        var eventNotify = '' + varr;
        //displaystatus('WEBPHONE SAY: ' + eventNotify);
        // skip this status in display STATUS,-1,Registered.
        //if (varr != 'STATUS,-1,Registered.') { displaystatus(eventNotify); }
        checkPhoneStatus(varr)

        
    }


    function voipRegister(server, username, password) {
        initcheck();
       // displaystatus('JAVASCRIPT SAY: register');
        applethandle.API_Register(server, username, password);
    }

    function voipCall(number) {
        initcheck();
        //displaystatus('JAVASCRIPT SAY: call');
        var theStatus = applethandle.API_GetStatus(-2);
        if (trim(theStatus) == "Registered.") {
        applethandle.API_Call(-1, number);
        }
        
    }

    function voipHangup() {
        initcheck();
        //displaystatus('JAVASCRIPT SAY: hangup');
        applethandle.API_Hangup(-2);
    }


    function voipSetSound() {
        initcheck();
        //displaystatus('JAVASCRIPT SAY: Set Sound');
        applethandle.API_AudioDevice();
    }

    function voipHold() {
        initcheck();
        //displaystatus('JAVASCRIPT SAY: Hold');
        applethandle.API_Hold(-1,true);
    }

    function voipUNHold() {
        initcheck();
        //displaystatus('JAVASCRIPT SAY: UnHold');
        applethandle.API_Hold(-1, false);
    }

    function voipChat() {
        initcheck();
       // displaystatus('JAVASCRIPT SAY: Chat');
        applethandle.API_Chat("1");
    }

    function voipDTMF(theString) {
        initcheck();
       // displaystatus('JAVASCRIPT SAY: DTMF ' + theString);
        applethandle.API_Dtmf(-1, theString);
    }

    function voipTransfer(thenumber) {
        initcheck();
       // displaystatus('JAVASCRIPT SAY: transfer to ' + thenumber);
        applethandle.API_Transfer(-1, thenumber);
    }

    function voipTransferDialog() {
        initcheck();
       // displaystatus('JAVASCRIPT SAY: transfer ');
        applethandle.API_TransferDialog();
    }