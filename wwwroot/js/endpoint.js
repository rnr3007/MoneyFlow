function changePage(element, pageNum) {
    if (!element.classList.contains('disabled')) {
        const query = window.location.search;
        const params = new URLSearchParams(query);
        params.set('page', pageNum);
        const newUrl = `${window.location.origin}${window.location.pathname}?${params.toString()}`

        window.location.href = newUrl;
    }
}