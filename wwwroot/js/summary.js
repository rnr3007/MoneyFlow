function loadChart(totalCostByDate) {
    const months = ['none', 'Jan', 'Feb', 'Mar', 'Apr', 'Jun', 'Jul', 'Aug', 'Sep', 'Nov', 'Okt', 'Des'];
    const labels = [];
    const myData = [];
    for (let i = 6; i >= 0; i--) {
        const dateSubtract = 24 * 3600 * 1000 * i;
        const dateNumber = new Date(new Date().setHours(0, 0, 0, 0) - dateSubtract + (24 * 3600 * 1000));
        const date = dateNumber.toISOString();
        labels.push(`${dateNumber.getDate()} ${months[dateNumber.getMonth()]}`);
        const foundData = totalCostByDate.find(x => 
            new Date(new Date(x.XAxis).getTime() + (24 * 3600 * 1000)).toISOString() == date
        );
        if (foundData) {
            myData.push(foundData.YAxis);
        } else {
            myData.push(0);
        }
    }

    const ctx = document.getElementById('lastWeekExpensesChartId');

    new Chart(ctx, {
        type: 'line',
        data: {
            labels,
            datasets: [
                {
                    label: 'Pengeluaran',
                    data: myData,
                    borderColor: '#BC202C',
                    backgroundColor: '#BC202C',
                }
            ]
        }
    })
}

function setBalloonPos(balloons, itemPrices, maxPrice) {
    var progressBar = document.getElementById('progressBarId');
    var progressWidth = progressBar.offsetWidth;
    for (let i = 0; i < balloons.length; i++) {
        const balloon = document.getElementById(balloons[i]);
        const position = ((itemPrices[i] / maxPrice) * progressWidth) - 50;
        balloon.style.setProperty("--balloon-position", `${position}px`);
    }

    console.log(balloons);
    console.log(itemPrices);
    console.log(maxPrice);

}