
// Function to update post content
const updatePostContent = (title, description, link) => {

    const titleElement = document.getElementById("title");
    const descriptionElement = document.getElementById("description");
    const linkElement = document.getElementById("link");
    const close = document.getElementById("close")

    close.textContent = title ? "X":''
    close.addEventListener("click", function () {
        getNews("null")
    })
    titleElement.textContent = title;
    descriptionElement.innerHTML = description;
    linkElement.href = link;
    linkElement.textContent = title?"Read more":'';
}

// Function to fetch news content and display it
const getNews = (title) => {
    console.log(title)
    $.ajax({
        type: "GET",
        url: "/GoogleNews/GetItem/"+title,
        data: { title: title },
        dataType: "json",
        success: function (data) {
            data == null ? updatePostContent(null, null, null) :
                updatePostContent(data.title, data.body, data.link);
        },
        error: function () {
            console.log("error fetching")
        }
    });
}

// Function to fetch news titles and display them
const getTitles = () => {
    $.ajax({
        type: "GET",
        url: "/GoogleNews/GetAllNews",
        contentType: "application/json; charset=utf-8", 
        dataType: "json",
        success: function (data) {
            const ul = document.getElementById("Titles");
            Titles = $("#Titles")
            data.forEach(item => {  
                var newsItemHtml = '<a href="#" class="title" data-title="' + item.title + '">' + item.title + '</a>';
                Titles.append(newsItemHtml);
            });
            Titles.on('click', '.title', function () {
                    getNews($(this).closest('.title').data('title'));
                });
        },
        error: function () {
            alert("Error fetching news.");
        }
    });
}

window.onload = getTitles;