document.getElementById('scrollToTop').addEventListener('click', function() {
window.scrollTo({ top: 0, behavior: 'smooth' });});

const openMenuButton = document.getElementById('openMenu');
    const closeMenuButton = document.getElementById('closeMenu');
    const openRightMenuButton = document.getElementById('openRightMenu');
    const contentMenuMobile = document.getElementById('contentMenuMobile');

    openMenuButton.addEventListener('click', () => {
      contentMenuMobile.classList.remove('transform', 'translate-x-full');
      openRightMenuButton.classList.remove('hidden');
    });

    closeMenuButton.addEventListener('click', () => {
      contentMenuMobile.classList.add('transform', 'translate-x-full');
      openRightMenuButton.classList.add('hidden');
    });

    openRightMenuButton.addEventListener('click', () => { 
      });

      const headerContainer = document.querySelector('.header-container');
      let isHeaderSticky = false;
  
      window.addEventListener('scroll', () => {
          console.log('Scroll Y:', window.scrollY);
          if (window.scrollY > 0 && !isHeaderSticky) {
              console.log('Sticky Header Activated');
              headerContainer.classList.remove('bg-transparent', 'text-white');
              headerContainer.classList.add('bg-rose-400', 'text-black','shadow-light');
              isHeaderSticky = true;
          } else if (window.scrollY === 0 && isHeaderSticky) {
              console.log('Sticky Header Deactivated');
              headerContainer.classList.remove('bg-rose-400', 'text-black');
              headerContainer.classList.add('bg-transparent', 'text-white');
              isHeaderSticky = false;
          }
      });
      document.addEventListener("DOMContentLoaded", function() {
        const isAdmin = false;
        const manageSection = document.getElementById("manageSection");
        const manageSectionMobile = document.getElementById("manageSectionMobile");

        if (!isAdmin) {
            manageSection.style.display = "none";
            manageSectionMobile.style.display = "none";
        }
    });
    // Kiểm tra trạng thái đăng nhập
    var isLoggedIn = false; // Đặt giá trị này dựa trên trạng thái đăng nhập của bạn

    // Ẩn hoặc hiện biểu tượng và menu tùy chọn dựa trên trạng thái đăng nhập
    function updateNavbar() {
        var iconLink = document.getElementById('iconLink');
        var userDropdown = document.getElementById('userDropdown');
        var userDropdownOptions = document.getElementById('userDropdownOptions');
        var iconLink1 = document.getElementById('iconLink-mobile');
        var userDropdown1 = document.getElementById('userDropdown-mobile');
        var userDropdownOptions1 = document.getElementById('userDropdownOptions-mobile');

        if (isLoggedIn) {
            iconLink.style.display = 'none'; // Ẩn biểu tượng
            userDropdown.classList.remove('hidden');
            iconLink1.style.display = 'none'; // Ẩn biểu tượng
            userDropdown1.classList.remove('hidden');
        } else {
            iconLink.style.display = 'block'; // Hiện biểu tượng
            userDropdown.classList.add('hidden');
            userDropdownOptions.classList.add('hidden');
            iconLink1.style.display = 'block'; // Hiện biểu tượng
            userDropdown1.classList.add('hidden');
            userDropdownOptions1.classList.add('hidden');
        }
    }

    // Hiển thị hoặc ẩn menu tùy chọn khi nhấp vào hình tròn tài khoản
    function toggleDropdown() {
        var dropdown = document.getElementById('userDropdownOptions');
        var dropdown1 = document.getElementById('userDropdownOptions-mobile');
        dropdown.classList.toggle('hidden');
        dropdown1.classList.toggle('hidden');
    }

    // Gọi hàm cập nhật khi trang được tải
    updateNavbar();
    let currentSlide = 0;
    const slideWidth = 300; // Chiều rộng của mỗi cột tin tức
    const marginBetweenColumns = 50; // Margin giữa các cột
  
    function showSlide(index) {
      const container = document.getElementById('news-wrapper');
      const translateValue = -(index * (slideWidth + marginBetweenColumns));
      container.style.transform = `translateX(${translateValue}px)`;
      currentSlide = index;
      updateButtonVisibility();
    }
  
    function prevSlide() {
      currentSlide = Math.max(currentSlide - 1, 0);
      showSlide(currentSlide);
    }
  
    function nextSlide() {
      const totalSlides = document.querySelectorAll('#news-wrapper > div').length;
  
      // Kiểm tra xem có thể tiến lên được không
      if (currentSlide < totalSlides - 3) {
        currentSlide++;
        showSlide(currentSlide);
      }
    }
  
    function updateButtonVisibility() {
      const nextButton = document.querySelector(".absolute .focus\\:outline-none ~ .focus\\:outline-none");
      const totalSlides = document.querySelectorAll('#news-wrapper > div').length;
  
      // Ẩn nút next ở cột cuối cùng
      if (currentSlide === totalSlides - 1) {
        nextButton.style.visibility = "hidden";
      } else {
        nextButton.style.visibility = "visible";
      }
    }
  
    // Hiển thị tin tức đầu tiên khi trang web được tải
    showSlide(currentSlide);