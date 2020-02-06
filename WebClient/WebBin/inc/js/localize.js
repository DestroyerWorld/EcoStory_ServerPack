tObject = {};
$(function(){
  getLang();
  var lang = localStorage.getItem('lang');
  if(lang == null)
    lang = navigator.language.substring(0, 2);
  else
    $("#langSelect").val(lang);
    setTimeout(function(){
        return  getTranslation(lang);
      },500
    )
});
function getLang(){
  $.ajax({
    type: "GET",
    url:  APPIQUERYURL + "/info",
    success:function(data) {
      var language = data.Language;
      var lang = '';
      switch(language) {
        case 'English':
          lang = 'en';
          break;
        case 'Spanish':
          lang = 'es';
          break;
        case 'Russian':
          lang = 'ru';
          break;
        case 'Czech':
          lang = 'cs';
          break;
        case 'Danish':
          lang = 'ds';
          break;
        case 'German':
          lang = 'de';
          break;
        case 'Gibberish':
          lang = 'el';
          break;
        case 'Español-Castillan':
          lang = 'es-ES';
          break;
        case 'French':
          lang = 'fr';
          break;
        case 'Hungarian':
          lang = 'hu';
          break;
        case 'Íslenska':
          lang = 'is';
          break;
        case 'Italian':
          lang = 'it';
          break;
        case 'Japanese':
          lang = 'ja';
          break;
        case 'Korean':
          lang = 'ko';
          break;
        case 'Dutch':
          lang = 'nl';
          break;
        case 'Norwegian':
          lang = 'no';
          break;
        case 'Polish':
          lang = 'pl';
          break;
        case 'BrazillianPortuguese':
          lang = 'pt-BR';
          break;
        case 'Portuguese':
          lang = 'pt-PT';
          break;
        case 'Romanian':
          lang = 'ro';
          break;
        case 'Swedish':
          lang = 'sv';
          break;
        case 'Ukrainian':
          lang = 'uk';
          break;
        case 'SimplifedChinese':
          lang = 'zh-CN';
          break;
      }
      localStorage.setItem('lang',lang)
    }
  })
}

function getTranslation(lang){

  $.ajax({
     type: "GET",
     url: "/i18n/" + lang+ "/WebClientStrings.csv",
     dataType: "text",
     success: function(data) {
       var allTextLines = data.split(/\r\n|\n/);

        allTextLines.forEach(function(element) {
         var pair = element.split(";")
         tObject[pair[0]] = pair[1];
        });
        translate(tObject, ".localize");
        localStorage.setItem('title',tObject[151]);
        localStorage.setItem('yes',tObject[78]);
        localStorage.setItem('no',tObject[79]);
        $('.setlang').attr('href','inc/css/languages/'+lang+'_design.css');
     },
     error: function(data){
       tObject = {}
       translate(tObject, ".localize");
     }
  });
}

function translate(tObject, tClass){
  $(tClass).each(function( index ) {
    var translateKey = $(this).attr('translate-key');
    var translation = tObject[translateKey];
    if(translation != undefined){
      if($(this).attr('placeholder')) {
        $(this).attr('placeholder', translation);
      } else {
        $(this).html(translation);
      }
    } else {
      if(!$(this).attr('placeholder') && !$(this).html()) {
        $(this).html(translateKey);
      }
    }
  });
}

function setLang(newLang){
  localStorage.setItem('lang', newLang);
  getTranslation(newLang);
}