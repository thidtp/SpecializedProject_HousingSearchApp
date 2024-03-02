document.getElementById('scrollToTop').addEventListener('click', function () {
    window.scrollTo({ top: 0, behavior: 'smooth' });
});
const headerContainer = document.querySelector('.header-container');
let isHeaderSticky = false;

window.addEventListener('scroll', () => {
    console.log('Scroll Y:', window.scrollY);
    if (window.scrollY > 0 && !isHeaderSticky) {
        console.log('Sticky Header Activated');
        headerContainer.classList.remove('bg-transparent', 'text-white');
        headerContainer.classList.add('bg-rose-400', 'text-black', 'shadow-light');
        isHeaderSticky = true;
    } else if (window.scrollY === 0 && isHeaderSticky) {
        console.log('Sticky Header Deactivated');
        headerContainer.classList.remove('bg-rose-400', 'text-black');
        headerContainer.classList.add('bg-transparent', 'text-white');
        isHeaderSticky = false;
    }
});
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
document.addEventListener("DOMContentLoaded", function () {
    const isAdmin = false;
    const manageSection = document.getElementById("manageSection");
    const manageSectionMobile = document.getElementById("manageSectionMobile");

    if (!isAdmin) {
        manageSection.style.display = "none";
        manageSectionMobile.style.display = "none";
    }
});
document.addEventListener('DOMContentLoaded', function () {
    // Kiểm tra xem trạng thái đăng nhập đã được đặt hay chưa
    var isLoggedIn = checkIfLoggedIn();

    // Lấy các phần tử DOM cần thiết
    var iconLink = document.getElementById('iconLink');
    var userDropdown = document.getElementById('userDropdown');
    var iconLinkMobile = document.getElementById('iconLink-mobile');
    var userDropdownMobile = document.getElementById('userDropdown-mobile');
    var userAvatar = document.getElementById('userAvatar');
    var accountName = document.getElementById('accountName');

    // Nếu đã đăng nhập, hiển thị thông tin người dùng và ẩn biểu tượng, hiển thị menu người dùng
    if (isLoggedIn) {
        displayUserInfo();
    } else {
        userAvatar.innerHTML = `<img src=~/Image/DuLieu/NguoiDung/"${avatar}" alt="Hình đại diện người dùng" class="h-full w-full">`;
        accountName.innerText = username;
        // Nếu chưa đăng nhập, hiển thị biểu tượng và ẩn menu người dùng
        iconLink.style.display = 'block';
        userDropdown.classList.add('hidden');
        iconLinkMobile.style.display = 'block';
        userDropdownMobile.classList.add('hidden');
    }
});

function checkIfLoggedIn() {
    // Kiểm tra xem trạng thái đăng nhập đã được đặt hay chưa
    // Sử dụng TempData từ Razor View để xác định trạng thái đăng nhập
    var isLoggedInTempData = "@TempData["DangNhapThanhCong"]";
    return isLoggedInTempData === "true";
}

function displayUserInfo() {


    // Ẩn biểu tượng và hiển thị menu người dùng
    iconLink.style.display = 'none';
    userDropdown.classList.remove('hidden');
    iconLinkMobile.style.display = 'none';
    userDropdownMobile.classList.remove('hidden');
}
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