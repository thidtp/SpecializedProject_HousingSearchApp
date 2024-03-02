// Owlcarousel
$(document).ready(function(){
    $(".owl-carousel").owlCarousel({
      loop:true,
      margin:10,
      nav:true,
      autoplay:false,
      autoplayTimeout:3000,
      autoplayHoverPause:true,
      center: false,
      navText: [
          "<i class='fa fa-angle-left'></i>",
          "<i class='fa fa-angle-right'></i>"
      ],
      responsive:{
          0:{
              items:1
          },
          600:{
              items:1
          },
          1000:{
              items:3
          }
      }
    });
  });
  $(document).ready(function () {
    var title = $("#shorten-title").text(); 
    var shortenedTitle = title.length > 50 ? title.substring(0, 50)+ "..." : title;
    $("#shorten-title").text(shortenedTitle);
});