/**************** [1. Basic Chart] *******************/

//定義給BasicLine使用的controller
myapp.controller('01_myapp_BasicLineCtrl', function ($scope) {
    $scope.chartSeries = [
        {
            name: 'Tokyo',
            data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
        }, {
            name: 'New York',
            data: [-0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5]
        }, {
            name: 'Berlin',
            data: [-0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0]
        }, {
            name: 'London',
            data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
        }
    ];

    $scope.chartConfig = {
        title: {
            text: 'Monthly Average Temperature'
        },
        subtitle: {
            text: 'Source: WorldClimate.com'
        },
        xAxis: {
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
        },
        yAxis: {
            title: {
                text: 'Temperature (°C)'
            }
        },
        options: {
            chart: {
                type: 'line'
            },
            tooltip: {
                valueSuffix: '°C'
            }
        },
        series: $scope.chartSeries
    };
});

//定義給BasicArea使用的controller
myapp.controller('01_myapp_BasicAreaCtrl', function ($scope) {
    $scope.chartSeries = [
        {
            name: 'USA',
            data: [null, null, null, null, null, 6, 11, 32, 110, 235, 369, 640,
                1005, 1436, 2063, 3057, 4618, 6444, 9822, 15468, 20434, 24126,
                27387, 29459, 31056, 31982, 32040, 31233, 29224, 27342, 26662,
                26956, 27912, 28999, 28965, 27826, 25579, 25722, 24826, 24605,
                24304, 23464, 23708, 24099, 24357, 24237, 24401, 24344, 23586,
                22380, 21004, 17287, 14747, 13076, 12555, 12144, 11009, 10950,
                10871, 10824, 10577, 10527, 10475, 10421, 10358, 10295, 10104]
        }, {
            name: 'USSR/Russia',
            data: [null, null, null, null, null, null, null, null, null, null,
                5, 25, 50, 120, 150, 200, 426, 660, 869, 1060, 1605, 2471, 3322,
                4238, 5221, 6129, 7089, 8339, 9399, 10538, 11643, 13092, 14478,
                15915, 17385, 19055, 21205, 23044, 25393, 27935, 30062, 32049,
                33952, 35804, 37431, 39197, 45000, 43000, 41000, 39000, 37000,
                35000, 33000, 31000, 29000, 27000, 25000, 24000, 23000, 22000,
                21000, 20000, 19000, 18000, 18000, 17000, 16000]
        }
    ];

    $scope.chartConfig = {
        title: {
            text: 'US and USSR nuclear stockpiles'
        },
        subtitle: {
            text: 'Source: <a href="http://thebulletin.metapress.com/content/c4120650912x74k7/fulltext.pdf">' +
                'thebulletin.metapress.com</a>'
        },
        xAxis: {
            allowDecimals: false,
            labels: {
                formatter: function () {
                    return this.value; // clean, unformatted number for year
                }
            }
        },
        yAxis: {
            title: {
                text: 'Nuclear weapon states'
            },
            labels: {
                formatter: function () {
                    return this.value / 1000 + 'k';
                }
            }
        },
        options: {
            chart: {
                type: 'area'
            },
            tooltip: {
                pointFormat: '{series.name} produced <b>{point.y:,.0f}</b><br/>warheads in {point.x}'
            },
            plotOptions: {
                area: {
                    pointStart: 1940,
                    marker: {
                        enabled: false,
                        symbol: 'circle',
                        radius: 2,
                        states: {
                            hover: {
                                enabled: true
                            }
                        }
                    }
                }
            }
        },
        series: $scope.chartSeries
    };
});

//定義給BasicBar使用的controller
myapp.controller('01_myapp_BasicBarCtrl', function ($scope) {
    $scope.chartSeries = [
        {
            name: 'Year 1800',
            data: [107, 31, 635, 203, 2]
        }, {
            name: 'Year 1900',
            data: [133, 156, 947, 408, 6]
        }, {
            name: 'Year 2008',
            data: [973, 914, 4054, 732, 34]
        }
    ];

    $scope.chartConfig = {
        title: {
            text: 'Historic World Population by Region'
        },
        subtitle: {
            text: 'Source: Wikipedia.org'
        },
        xAxis: {
            categories: ['Africa', 'America', 'Asia', 'Europe', 'Oceania'],
            title: {
                text: null
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Population (millions)',
                align: 'high'
            },
            labels: {
                overflow: 'justify'
            }
        },
        credits: {
            enabled: false
        },
        options: {
            chart: {
                type: 'bar'
            },
            tooltip: {
                valueSuffix: ' millions'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -40,
                y: 100,
                floating: true,
                borderWidth: 1,
                backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
                shadow: true
            },
        },
        series: $scope.chartSeries
    };
});

//定義給BasicColumn使用的controller
myapp.controller('01_myapp_BasicColumnCtrl', function ($scope) {
    $scope.chartSeries = [
        {
            name: 'Tokyo',
            data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4]
        }, {
            name: 'New York',
            data: [83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5, 106.6, 92.3]
        }, {
            name: 'London',
            data: [48.9, 38.8, 39.3, 41.4, 47.0, 48.3, 59.0, 59.6, 52.4, 65.2, 59.3, 51.2]
        }, {
            name: 'Berlin',
            data: [42.4, 33.2, 34.5, 39.7, 52.6, 75.5, 57.4, 60.4, 47.6, 39.1, 46.8, 51.1]
        }
    ];

    $scope.chartConfig = {
        title: {
            text: 'Monthly Average Rainfall'
        },
        subtitle: {
            text: 'Source: WorldClimate.com'
        },
        xAxis: {
            categories: [
                'Jan',
                'Feb',
                'Mar',
                'Apr',
                'May',
                'Jun',
                'Jul',
                'Aug',
                'Sep',
                'Oct',
                'Nov',
                'Dec'
            ]
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Rainfall (mm)'
            }
        },
        credits: {
            enabled: false
        },
        options: {
            chart: {
                type: 'column'
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                    '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            }
        },
        series: $scope.chartSeries
    };
});

//定義給BasicPie使用的controller
myapp.controller('01_myapp_BasicPieCtrl', function ($scope) {
    $scope.chartSeries = [{
        type: 'pie',
        name: 'Browser share',
        data: [
            ['Firefox', 45.0],
            ['IE', 26.8],
            {
                name: 'Chrome',
                y: 12.8,
                sliced: true,
                selected: true
            },
            ['Safari', 8.5],
            ['Opera', 6.2],
            {
                name: 'Others',
                y: 0.7,
                dataLabels: {
                    enabled: false
                }
            }
        ]
    }];

    $scope.chartConfig = {
        title: {
            text: 'Browser market shares at a specific website, 2014'
        },
        credits: {
            enabled: false
        },
        options: {
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        distance: -50,
                        style: {
                            fontWeight: 'bold',
                            color: 'white',
                            textShadow: '0px 1px 2px black'
                        }
                    },
                }
            }
        },
        series: $scope.chartSeries
    };
});

//定義給BasicDonut使用的controller
myapp.controller('01_myapp_BasicDonutCtrl', function ($scope) {
    $scope.chartSeries = [{
        type: 'pie',
        name: 'Browser share',
        innerSize: '50%',
        data: [
            ['Firefox', 45.0],
            ['IE', 26.8],
            ['Chrome', 12.8],
            ['Safari', 8.5],
            ['Opera', 6.2],
            {
                name: 'Others',
                y: 0.7,
                dataLabels: {
                    enabled: false
                }
            }
        ]
    }];

    $scope.chartConfig = {
        title: {
            text: 'Browser market shares at a specific website, 2014'
        },
        credits: {
            enabled: false
        },
        options: {
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: 0,
                plotShadow: false
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            },
            plotOptions: {
                pie: {
                    dataLabels: {
                        enabled: true,
                        distance: -50,
                        style: {
                            fontWeight: 'bold',
                            color: 'white',
                            textShadow: '0px 1px 2px black'
                        }
                    },
                    startAngle: -90,
                    endAngle: 90,
                    center: ['50%', '75%']
                }
            },
        },
        series: $scope.chartSeries
    };
});

//定義給ScatterPlot使用的controller
myapp.controller('01_myapp_ScatterPlotCtrl', function ($scope) {
    $scope.chartSeries = [{
        name: 'Female',
        color: 'rgba(223, 83, 83, .5)',
        data: [[161.2, 51.6], [167.5, 59.0], [159.5, 49.2], [157.0, 63.0], [155.8, 53.6],
            [170.0, 59.0], [159.1, 47.6], [166.0, 69.8], [176.2, 66.8], [160.2, 75.2],
            [172.5, 55.2], [170.9, 54.2], [172.9, 62.5], [153.4, 42.0], [160.0, 50.0],
            [147.2, 49.8], [168.2, 49.2], [175.0, 73.2], [157.0, 47.8], [167.6, 68.8],
            [159.5, 50.6], [175.0, 82.5], [166.8, 57.2], [176.5, 87.8], [170.2, 72.8],
            [174.0, 54.5], [173.0, 59.8], [179.9, 67.3], [170.5, 67.8], [160.0, 47.0],
            [154.4, 46.2], [162.0, 55.0], [176.5, 83.0], [160.0, 54.4], [152.0, 45.8],
            [162.1, 53.6], [170.0, 73.2], [160.2, 52.1], [161.3, 67.9], [166.4, 56.6],
            [168.9, 62.3], [163.8, 58.5], [167.6, 54.5], [160.0, 50.2], [161.3, 60.3],
            [167.6, 58.3], [165.1, 56.2], [160.0, 50.2], [170.0, 72.9], [157.5, 59.8],
            [167.6, 61.0], [160.7, 69.1], [163.2, 55.9], [152.4, 46.5], [157.5, 54.3],
            [168.3, 54.8], [180.3, 60.7], [165.5, 60.0], [165.0, 62.0], [164.5, 60.3],
            [156.0, 52.7], [160.0, 74.3], [163.0, 62.0], [165.7, 73.1], [161.0, 80.0],
            [162.0, 54.7], [166.0, 53.2], [174.0, 75.7], [172.7, 61.1], [167.6, 55.7],
            [151.1, 48.7], [164.5, 52.3], [163.5, 50.0], [152.0, 59.3], [169.0, 62.5],
            [164.0, 55.7], [161.2, 54.8], [155.0, 45.9], [170.0, 70.6], [176.2, 67.2],
            [170.0, 69.4], [162.5, 58.2], [170.3, 64.8], [164.1, 71.6], [169.5, 52.8],
            [163.2, 59.8], [154.5, 49.0], [159.8, 50.0], [173.2, 69.2], [170.0, 55.9],
            [161.4, 63.4], [169.0, 58.2], [166.2, 58.6], [159.4, 45.7], [162.5, 52.2],
            [159.0, 48.6], [162.8, 57.8], [159.0, 55.6], [179.8, 66.8], [162.9, 59.4],
            [161.0, 53.6], [151.1, 73.2], [168.2, 53.4], [168.9, 69.0], [173.2, 58.4],
            [171.8, 56.2], [178.0, 70.6], [164.3, 59.8], [163.0, 72.0], [168.5, 65.2],
            [166.8, 56.6], [172.7, 105.2], [163.5, 51.8], [169.4, 63.4], [167.8, 59.0],
            [159.5, 47.6], [167.6, 63.0], [161.2, 55.2], [160.0, 45.0], [163.2, 54.0],
            [162.2, 50.2], [161.3, 60.2], [149.5, 44.8], [157.5, 58.8], [163.2, 56.4],
            [172.7, 62.0], [155.0, 49.2], [156.5, 67.2], [164.0, 53.8], [160.9, 54.4],
            [162.8, 58.0], [167.0, 59.8], [160.0, 54.8], [160.0, 43.2], [168.9, 60.5],
            [158.2, 46.4], [156.0, 64.4], [160.0, 48.8], [167.1, 62.2], [158.0, 55.5],
            [167.6, 57.8], [156.0, 54.6], [162.1, 59.2], [173.4, 52.7], [159.8, 53.2],
            [170.5, 64.5], [159.2, 51.8], [157.5, 56.0], [161.3, 63.6], [162.6, 63.2],
            [160.0, 59.5], [168.9, 56.8], [165.1, 64.1], [162.6, 50.0], [165.1, 72.3],
            [166.4, 55.0], [160.0, 55.9], [152.4, 60.4], [170.2, 69.1], [162.6, 84.5],
            [170.2, 55.9], [158.8, 55.5], [172.7, 69.5], [167.6, 76.4], [162.6, 61.4],
            [167.6, 65.9], [156.2, 58.6], [175.2, 66.8], [172.1, 56.6], [162.6, 58.6],
            [160.0, 55.9], [165.1, 59.1], [182.9, 81.8], [166.4, 70.7], [165.1, 56.8],
            [177.8, 60.0], [165.1, 58.2], [175.3, 72.7], [154.9, 54.1], [158.8, 49.1],
            [172.7, 75.9], [168.9, 55.0], [161.3, 57.3], [167.6, 55.0], [165.1, 65.5],
            [175.3, 65.5], [157.5, 48.6], [163.8, 58.6], [167.6, 63.6], [165.1, 55.2],
            [165.1, 62.7], [168.9, 56.6], [162.6, 53.9], [164.5, 63.2], [176.5, 73.6],
            [168.9, 62.0], [175.3, 63.6], [159.4, 53.2], [160.0, 53.4], [170.2, 55.0],
            [162.6, 70.5], [167.6, 54.5], [162.6, 54.5], [160.7, 55.9], [160.0, 59.0],
            [157.5, 63.6], [162.6, 54.5], [152.4, 47.3], [170.2, 67.7], [165.1, 80.9],
            [172.7, 70.5], [165.1, 60.9], [170.2, 63.6], [170.2, 54.5], [170.2, 59.1],
            [161.3, 70.5], [167.6, 52.7], [167.6, 62.7], [165.1, 86.3], [162.6, 66.4],
            [152.4, 67.3], [168.9, 63.0], [170.2, 73.6], [175.2, 62.3], [175.2, 57.7],
            [160.0, 55.4], [165.1, 104.1], [174.0, 55.5], [170.2, 77.3], [160.0, 80.5],
            [167.6, 64.5], [167.6, 72.3], [167.6, 61.4], [154.9, 58.2], [162.6, 81.8],
            [175.3, 63.6], [171.4, 53.4], [157.5, 54.5], [165.1, 53.6], [160.0, 60.0],
            [174.0, 73.6], [162.6, 61.4], [174.0, 55.5], [162.6, 63.6], [161.3, 60.9],
            [156.2, 60.0], [149.9, 46.8], [169.5, 57.3], [160.0, 64.1], [175.3, 63.6],
            [169.5, 67.3], [160.0, 75.5], [172.7, 68.2], [162.6, 61.4], [157.5, 76.8],
            [176.5, 71.8], [164.4, 55.5], [160.7, 48.6], [174.0, 66.4], [163.8, 67.3]]

    }, {
        name: 'Male',
        color: 'rgba(119, 152, 191, .5)',
        data: [[174.0, 65.6], [175.3, 71.8], [193.5, 80.7], [186.5, 72.6], [187.2, 78.8],
            [181.5, 74.8], [184.0, 86.4], [184.5, 78.4], [175.0, 62.0], [184.0, 81.6],
            [180.0, 76.6], [177.8, 83.6], [192.0, 90.0], [176.0, 74.6], [174.0, 71.0],
            [184.0, 79.6], [192.7, 93.8], [171.5, 70.0], [173.0, 72.4], [176.0, 85.9],
            [176.0, 78.8], [180.5, 77.8], [172.7, 66.2], [176.0, 86.4], [173.5, 81.8],
            [178.0, 89.6], [180.3, 82.8], [180.3, 76.4], [164.5, 63.2], [173.0, 60.9],
            [183.5, 74.8], [175.5, 70.0], [188.0, 72.4], [189.2, 84.1], [172.8, 69.1],
            [170.0, 59.5], [182.0, 67.2], [170.0, 61.3], [177.8, 68.6], [184.2, 80.1],
            [186.7, 87.8], [171.4, 84.7], [172.7, 73.4], [175.3, 72.1], [180.3, 82.6],
            [182.9, 88.7], [188.0, 84.1], [177.2, 94.1], [172.1, 74.9], [167.0, 59.1],
            [169.5, 75.6], [174.0, 86.2], [172.7, 75.3], [182.2, 87.1], [164.1, 55.2],
            [163.0, 57.0], [171.5, 61.4], [184.2, 76.8], [174.0, 86.8], [174.0, 72.2],
            [177.0, 71.6], [186.0, 84.8], [167.0, 68.2], [171.8, 66.1], [182.0, 72.0],
            [167.0, 64.6], [177.8, 74.8], [164.5, 70.0], [192.0, 101.6], [175.5, 63.2],
            [171.2, 79.1], [181.6, 78.9], [167.4, 67.7], [181.1, 66.0], [177.0, 68.2],
            [174.5, 63.9], [177.5, 72.0], [170.5, 56.8], [182.4, 74.5], [197.1, 90.9],
            [180.1, 93.0], [175.5, 80.9], [180.6, 72.7], [184.4, 68.0], [175.5, 70.9],
            [180.6, 72.5], [177.0, 72.5], [177.1, 83.4], [181.6, 75.5], [176.5, 73.0],
            [175.0, 70.2], [174.0, 73.4], [165.1, 70.5], [177.0, 68.9], [192.0, 102.3],
            [176.5, 68.4], [169.4, 65.9], [182.1, 75.7], [179.8, 84.5], [175.3, 87.7],
            [184.9, 86.4], [177.3, 73.2], [167.4, 53.9], [178.1, 72.0], [168.9, 55.5],
            [157.2, 58.4], [180.3, 83.2], [170.2, 72.7], [177.8, 64.1], [172.7, 72.3],
            [165.1, 65.0], [186.7, 86.4], [165.1, 65.0], [174.0, 88.6], [175.3, 84.1],
            [185.4, 66.8], [177.8, 75.5], [180.3, 93.2], [180.3, 82.7], [177.8, 58.0],
            [177.8, 79.5], [177.8, 78.6], [177.8, 71.8], [177.8, 116.4], [163.8, 72.2],
            [188.0, 83.6], [198.1, 85.5], [175.3, 90.9], [166.4, 85.9], [190.5, 89.1],
            [166.4, 75.0], [177.8, 77.7], [179.7, 86.4], [172.7, 90.9], [190.5, 73.6],
            [185.4, 76.4], [168.9, 69.1], [167.6, 84.5], [175.3, 64.5], [170.2, 69.1],
            [190.5, 108.6], [177.8, 86.4], [190.5, 80.9], [177.8, 87.7], [184.2, 94.5],
            [176.5, 80.2], [177.8, 72.0], [180.3, 71.4], [171.4, 72.7], [172.7, 84.1],
            [172.7, 76.8], [177.8, 63.6], [177.8, 80.9], [182.9, 80.9], [170.2, 85.5],
            [167.6, 68.6], [175.3, 67.7], [165.1, 66.4], [185.4, 102.3], [181.6, 70.5],
            [172.7, 95.9], [190.5, 84.1], [179.1, 87.3], [175.3, 71.8], [170.2, 65.9],
            [193.0, 95.9], [171.4, 91.4], [177.8, 81.8], [177.8, 96.8], [167.6, 69.1],
            [167.6, 82.7], [180.3, 75.5], [182.9, 79.5], [176.5, 73.6], [186.7, 91.8],
            [188.0, 84.1], [188.0, 85.9], [177.8, 81.8], [174.0, 82.5], [177.8, 80.5],
            [171.4, 70.0], [185.4, 81.8], [185.4, 84.1], [188.0, 90.5], [188.0, 91.4],
            [182.9, 89.1], [176.5, 85.0], [175.3, 69.1], [175.3, 73.6], [188.0, 80.5],
            [188.0, 82.7], [175.3, 86.4], [170.5, 67.7], [179.1, 92.7], [177.8, 93.6],
            [175.3, 70.9], [182.9, 75.0], [170.8, 93.2], [188.0, 93.2], [180.3, 77.7],
            [177.8, 61.4], [185.4, 94.1], [168.9, 75.0], [185.4, 83.6], [180.3, 85.5],
            [174.0, 73.9], [167.6, 66.8], [182.9, 87.3], [160.0, 72.3], [180.3, 88.6],
            [167.6, 75.5], [186.7, 101.4], [175.3, 91.1], [175.3, 67.3], [175.9, 77.7],
            [175.3, 81.8], [179.1, 75.5], [181.6, 84.5], [177.8, 76.6], [182.9, 85.0],
            [177.8, 102.5], [184.2, 77.3], [179.1, 71.8], [176.5, 87.9], [188.0, 94.3],
            [174.0, 70.9], [167.6, 64.5], [170.2, 77.3], [167.6, 72.3], [188.0, 87.3],
            [174.0, 80.0], [176.5, 82.3], [180.3, 73.6], [167.6, 74.1], [188.0, 85.9],
            [180.3, 73.2], [167.6, 76.3], [183.0, 65.9], [183.0, 90.9], [179.1, 89.1],
            [170.2, 62.3], [177.8, 82.7], [179.1, 79.1], [190.5, 98.2], [177.8, 84.1],
            [180.3, 83.2], [180.3, 83.2]]
    }];

    $scope.chartConfig = {
        title: {
            text: 'Height Versus Weight of 507 Individuals by Gender'
        },
        subtitle: {
            text: 'Source: Heinz  2003'
        },
        xAxis: {
            title: {
                enabled: true,
                text: 'Height (cm)'
            },
            startOnTick: true,
            endOnTick: true,
            showLastLabel: true
        },
        yAxis: {
            title: {
                text: 'Weight (kg)'
            }
        },
        credits: {
            enabled: false
        },
        options: {
            chart: {
                type: 'scatter',
                zoomType: 'xy'
            },
            legend: {
                layout: 'vertical',
                align: 'left',
                verticalAlign: 'top',
                x: 100,
                y: 70,
                floating: true,
                backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF',
                borderWidth: 1
            },
            plotOptions: {
                scatter: {
                    marker: {
                        radius: 5,
                        states: {
                            hover: {
                                enabled: true,
                                lineColor: 'rgb(100,100,100)'
                            }
                        }
                    },
                    states: {
                        hover: {
                            marker: {
                                enabled: false
                            }
                        }
                    },
                    tooltip: {
                        headerFormat: '<b>{series.name}</b><br>',
                        pointFormat: '{point.x} cm, {point.y} kg'
                    }
                }
            }
        },
        series: $scope.chartSeries
    };
});

//定義給BubbleChart使用的controller
myapp.controller('01_myapp_BubbleChartCtrl', function ($scope) {
    $scope.chartSeries = [
        {
            data: [[97, 36, 79], [94, 74, 60], [68, 76, 58], [64, 87, 56], [68, 27, 73], [74, 99, 42], [7, 93, 87], [51, 69, 40], [38, 23, 33], [57, 86, 31]]
        }, {
            data: [[25, 10, 87], [2, 75, 59], [11, 54, 8], [86, 55, 93], [5, 3, 58], [90, 63, 44], [91, 33, 17], [97, 3, 56], [15, 67, 48], [54, 25, 81]]
        }, {
            data: [[47, 47, 21], [20, 12, 4], [6, 76, 91], [38, 30, 60], [57, 98, 64], [61, 17, 80], [83, 60, 13], [67, 78, 75], [64, 12, 10], [30, 77, 82]]
        }
    ];

    $scope.chartConfig = {
        title: {
            text: 'Highcharts Bubbles'
        },
        credits: {
            enabled: false
        },
        options: {
            chart: {
                type: 'bubble',
                zoomType: 'xy'
            }
        },
        series: $scope.chartSeries
    };
});

/**************** [2. Interaction Chart] *******************/

//定義給展示如何使用AngularJS來控制Highchart的controller
myapp.controller('02_myapp_ChartInteractionCtrl', function ($scope) {
    $scope.chartTypes = [
        { "id": "line", "title": "Line" },
        { "id": "spline", "title": "Smooth line" },
        { "id": "area", "title": "Area" },
        { "id": "areaspline", "title": "Smooth area" },
        { "id": "column", "title": "Column" },
        { "id": "bar", "title": "Bar" },
        { "id": "pie", "title": "Pie" },
        { "id": "scatter", "title": "Scatter" }
    ];

    $scope.dashStyles = [
        { "id": "Solid", "title": "Solid" },
        { "id": "ShortDash", "title": "ShortDash" },
        { "id": "ShortDot", "title": "ShortDot" },
        { "id": "ShortDashDot", "title": "ShortDashDot" },
        { "id": "ShortDashDotDot", "title": "ShortDashDotDot" },
        { "id": "Dot", "title": "Dot" },
        { "id": "Dash", "title": "Dash" },
        { "id": "LongDash", "title": "LongDash" },
        { "id": "DashDot", "title": "DashDot" },
        { "id": "LongDashDot", "title": "LongDashDot" },
        { "id": "LongDashDotDot", "title": "LongDashDotDot" }
    ];

    $scope.chartSeries = [
        { "name": "Series_01", "data": [1, 2, 4, 7, 3] },
        { "name": "Series_02", "data": [3, 1, null, 5, 2], connectNulls: true },
        { "name": "Series_03", "data": [5, 2, 2, 3, 5], type: "column" },
        { "name": "Series_04", "data": [1, 1, 2, 3, 2], type: "column" }
    ];

    $scope.chartStack = [
        { "id": '', "title": "No" },
        { "id": "normal", "title": "Normal" },
        { "id": "percent", "title": "Percent" }
    ];

    $scope.addPoints = function () {
        var seriesArray = $scope.chartConfig.series;
        var rndIdx = Math.floor(Math.random() * seriesArray.length);
        seriesArray[rndIdx].data = seriesArray[rndIdx].data.concat([1, 10, 20]);
    };

    $scope.addSeries = function () {
        var rnd = [];
        for (var i = 0; i < 10; i++) {
            rnd.push(Math.floor(Math.random() * 20) + 1);
        }
        $scope.chartConfig.series.push({
            data: rnd
        });
    };

    $scope.removeRandomSeries = function () {
        var seriesArray = $scope.chartConfig.series;
        var rndIdx = Math.floor(Math.random() * seriesArray.length);
        seriesArray.splice(rndIdx, 1)
    }

    $scope.removeSeries = function (id) {
        var seriesArray = $scope.chartConfig.series;
        seriesArray.splice(id, 1);
    };

    $scope.reflow = function () {
        $scope.$broadcast('highchartsng.reflow');
    };

    $scope.chartConfig = {
        title: {
            text: 'Interaction with Chart'
        },
        subtitle:{
            text: 'PracticalCoding'
        },
        credits: {
            enabled: false
        },
        loading: false,
        size: {},
        options: {
            chart: {
                type: 'areaspline'
            },
            plotOptions: {
                series: {
                    stacking: ''
                }
            }
        },
        series: $scope.chartSeries
    };

});

/**************** [3. Integration Chart] *******************/

//定義給整合AngularJS與Highchart的controller

// define a controller for 03_IntegrateDataTable
myapp.controller('03_myapp_integrateDataTableCtrl', function ($scope, ChartDataFactory, EventBusHub) {
    $scope.vm = {};
    $scope.vm.chartdatas = [];
    $scope.vm.chartdata = {}
    $scope.vm.chartdata.loading = true; //<--用來控制幕面的loading icons
    $scope.vm.alerts = []; //<--用來顯示CURD的結果訊息

    refreshData = function () {
        $scope.vm.chartdata.loading = true;
        //叫用ChartDataFactory的resetTrainingDatas的method來取得最新的資料
        //method的引數(args)是一個回call function
        ChartDataFactory.resetTrainingDatas(function (trainingdatas) {
            $scope.vm.chartdatas = trainingdatas;
            $scope.vm.chartdata.loading = false;
        });
    };

    //初始化
    refreshData();

    //查詢(Query) - delegate to ChartDataFactory
    $scope.getAllChartdatas = function () {
        refreshData();
    };

    //新增(Create) - delegate to ChartDataFactory
    $scope.createChartdata = function () {
        ChartDataFactory.createTrainingChartData(
            //第一個參數
            $scope.vm.chartdata,
            //第二個參數:Operation執行成功所呼叫的callbackFn
            function (data, status, headers, config) {
                $scope.vm.alerts.push({ type: 'success', msg: 'New chartdata [' + data + '] created successfully!' });
                $scope.vm.action.mode = 'NoOp';
                refreshData();
            },
            //第三個參數:Operation執行失敗所呼叫的callbackFn
            function (data, status, headers, config) {
                $scope.vm.alerts.push({ type: 'danger', msg: 'New chartdata created error!' })
            }
        );
    };

    //修改(Update) - delegate to ChartDataFactory
    $scope.updateChartdata = function () {
        ChartDataFactory.updateTrainingChartData(
           //第一個參數
           $scope.vm.chartdata,
           //第二個參數:Operation執行成功所呼叫的callbackFn
           function (data, status, headers, config) {
               $scope.vm.alerts.push({ type: 'success', msg: 'Update chartdata [' + $scope.vm.chartdata.Id + '] successfully!' });
               $scope.vm.action.mode = 'NoOp';
               refreshData();
           },
           //第三個參數:Operation執行失敗所呼叫的callbackFn
           function (data, status, headers, config) {
               $scope.vm.alerts.push({ type: 'danger', msg: 'Update chartdata [' + $scope.vm.chartdata.Id + '] error!' })
           }
       );
    };

    //刪除(Delete) - delegate to ChartDataFactory
    $scope.deleteChartdata = function () {
        //ChartDataFactory.deleteTrainingChartData($scope, $scope.vm.chartdata);
        ChartDataFactory.deleteTrainingChartData(
           //第一個參數
           $scope.vm.chartdata,
           //第二個參數:Operation執行成功所呼叫的callbackFn
           function (data, status, headers, config) {
               $scope.vm.alerts.push({ type: 'success', msg: 'Delete chartdata [' + $scope.vm.chartdata.Id + '] successfully!' });
               $scope.vm.action.mode = 'NoOp';
               refreshData();
           },
           //第三個參數:Operation執行失敗所呼叫的callbackFn
           function (data, status, headers, config) {
               $scope.vm.alerts.push({ type: 'danger', msg: 'Delete chartdata [' + $scope.vm.chartdata.Id + '] error!' })
           }
       );
    };

    //function to close specific alert message on the screen
    $scope.closeAlert = function (index) {
        $scope.vm.alerts.splice(index, 1);
    };

    //聽從$rootScope廣播下來的Event
    $scope.$on('chartdata_created', function (event, args) {        
        refreshData();
    });

    $scope.$on('chartdata_updated', function (event, args) {        
        refreshData();
    });

    $scope.$on('chartdata_deleted', function (event, args) {        
        refreshData();
    });
});

// define a controller for 03_IntegrateLine
myapp.controller('03_myapp_integrateLineCtrl', function ($scope, ChartDataFactory, EventBusHub) {
    //初始化圖表的function
    init = function () {
        ChartDataFactory.resetTrainingDatas(function () {
            //***Highchart圖表要秀的資料***//
            var chart = $scope.chartConfig.getHighcharts();

            chart.addSeries({
                    name: '景氣對策信號',
                    data: ChartDataFactory.getDataSeriesByProp("monitoringindex"),
                }, false);                  

            chart.redraw();
        })
    };   

    //圖表初始化
    init();

    //***Highchart圖表要秀的資料***//
    $scope.chartSeries = [];

    //***Highchart的圖表configuration***//
    $scope.chartConfig = {
        title: {
            text: '景氣對策信號',
            x: -20 //center
        },
        subtitle: {
            text: '來源: 國家發展委員會',
            x: -20
        },
        xAxis: {
            type: 'datetime'
        },
        yAxis: {
            title: {
                text: '分'
            },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
        options: {
            chart: {
                type: 'spline',
                zoomType: 'x'
            },
            tooltip: {
                valueSuffix: ' (分)'
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle',
                borderWidth: 0
            }
        },
        
        series: $scope.chartSeries
    };

    //聽從$rootScope廣播下來的Events
    $scope.$on('chartdata_created', function (event, args) {
        refreshChart();
    });

    $scope.$on('chartdata_updated', function (event, args) {
        refreshChart();
    });

    $scope.$on('chartdata_deleted', function (event, args) {
        refreshChart();
    });

    //重新refresh圖表
    refreshChart = function () {
        ChartDataFactory.resetTrainingDatas(function () {
            //***Highchart圖表要秀的資料***//
            //取得Highchart的chart物件instance
            var chart = $scope.chartConfig.getHighcharts();
            //重設每個資料序列的內容
            chart.series[0].setData(ChartDataFactory.getDataSeriesByProp("monitoringindex"), false);

            //要求Highchart重劃圖表
            chart.redraw();
        })
    };
});

// define a controller for 03_IntegrateDualAxes
myapp.controller('03_myapp_integrateDualAxesCtrl', function ($scope, ChartDataFactory, EventBusHub) {

    //初始化圖表的function
    init = function () {
        ChartDataFactory.resetTrainingDatas(function () {            
            //***Highchart圖表要秀的資料***//
            var chart = $scope.chartConfig.getHighcharts();

            chart.addSeries({
                name: '景氣對策信號',
                data: ChartDataFactory.getDataSeriesByProp("monitoringindex"),
                type: 'spline',
                marker: {
                    enabled: false
                }}, false);

            chart.addSeries({
                name: '景氣領先指標',
                data: ChartDataFactory.getDataSeriesByProp("leadingindex"),
                type: 'spline',
                yAxis: 1,
                type: 'spline',
                marker: {
                    enabled: false
                },
                dashStyle: 'shortdot'
            }, false);

            chart.addSeries({
                name: '景氣同時指標',
                data: ChartDataFactory.getDataSeriesByProp("coincidentindex"),
                type: 'spline',
                yAxis: 1,
                type: 'spline',
                marker: {
                    enabled: false
                },
                dashStyle: 'shortdot'
            }, false);

            chart.addSeries({
                name: '景氣落後指標',
                data: ChartDataFactory.getDataSeriesByProp("laggingindex"),
                type: 'spline',
                yAxis: 1,
                type: 'spline',
                marker: {
                    enabled: false
                },
                dashStyle: 'shortdot'
            }, false);

            chart.redraw();
        })
    };

    //圖表初始化
    init();    

    //***Highchart圖表要秀的資料***//
    $scope.chartSeries = [];

    //***Highchart的圖表configuration***//
    $scope.chartConfig = {
        title: {
            text: '景氣對策信號',
            x: -20 //center
        },
        subtitle: {
            text: '來源: 國家發展委員會',
            x: -20
        },
        xAxis: {
            type: 'datetime'
        },
        yAxis: [
            {   //主要y軸
                title: {
                    text: '景氣對策信號',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                },
                labels: {
                    format: '{value} 分',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                }
            },
            {   //第二y軸
                title: {
                    text: '景氣指標'
                },
                opposite: true //把這一軸放到右邊
            }
        ],
        options: {
            chart: {
                zoomType: 'xy'
            },
            tooltip: {
                valueSuffix: ' (分)'
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle',
                borderWidth: 0
            }
        },

        series: $scope.chartSeries
    };

    //聽從$rootScope廣播下來的Events
    $scope.$on('chartdata_created', function (event, args) {
        refreshChart();
    });

    $scope.$on('chartdata_updated', function (event, args) {
        refreshChart();
    });

    $scope.$on('chartdata_deleted', function (event, args) {
        refreshChart();
    });

    //重新refresh圖表
    refreshChart = function () {
        ChartDataFactory.resetTrainingDatas(function () {
            //***Highchart圖表要秀的資料***//
            //取得Highchart的chart物件instance
            var chart = $scope.chartConfig.getHighcharts();
            //重設每個資料序列的內容
            chart.series[0].setData(ChartDataFactory.getDataSeriesByProp("monitoringindex"), false);
            chart.series[1].setData(ChartDataFactory.getDataSeriesByProp("leadingindex"), false);
            chart.series[2].setData(ChartDataFactory.getDataSeriesByProp("coincidentindex"), false);
            chart.series[3].setData(ChartDataFactory.getDataSeriesByProp("laggingindex"), false);
            //要求Highchart重劃圖表
            chart.redraw();
        })        
    };
});

// define a controller for 03_IntegrateMultiAxes
myapp.controller('03_myapp_integrateMultiAxesCtrl', function ($scope, ChartDataFactory, EventBusHub) {

    //初始化圖表的function
    init = function () {
        ChartDataFactory.resetTrainingDatas(function () {
            //***Highchart圖表要秀的資料***//
            var chart = $scope.chartConfig.getHighcharts();

            chart.addSeries({
                name: '景氣對策信號',
                data: ChartDataFactory.getDataSeriesByProp("monitoringindex"),
                type: 'spline',
                marker: {
                    enabled: false
                }
            }, false);

            chart.addSeries({
                name: '景氣領先指標',
                data: ChartDataFactory.getDataSeriesByProp("leadingindex"),
                type: 'spline',
                yAxis: 1,
                type: 'spline',
                marker: {
                    enabled: false
                },
                dashStyle: 'shortdot'
            }, false);

            chart.addSeries({
                name: '景氣同時指標',
                data: ChartDataFactory.getDataSeriesByProp("coincidentindex"),
                type: 'spline',
                yAxis: 1,
                type: 'spline',
                marker: {
                    enabled: false
                },
                dashStyle: 'shortdot'
            }, false);

            chart.addSeries({
                name: '景氣落後指標',
                data: ChartDataFactory.getDataSeriesByProp("laggingindex"),
                type: 'spline',
                yAxis: 1,
                type: 'spline',
                marker: {
                    enabled: false
                },
                dashStyle: 'shortdot'
            }, false);

            chart.addSeries({
                name: '台股加權股價指數',
                data: ChartDataFactory.getDataSeriesByProp("taiex"),
                type: 'spline',
                yAxis: 2,
                type: 'spline',
                marker: {
                    enabled: false
                },
                color: Highcharts.getOptions().colors[5]
            }, false);

            chart.redraw();
        })
    };

    //圖表初始化
    init();

    //***Highchart圖表要秀的資料***//
    $scope.chartSeries = [];

    //***Highchart的圖表configuration***//
    $scope.chartConfig = {
        title: {
            text: '景氣對策信號',
            x: -20 //center
        },
        subtitle: {
            text: '來源: 國家發展委員會',
            x: -20
        },
        xAxis: {
            type: 'datetime'
        },
        yAxis: [
            {   //主要y軸
                title: {
                    text: '景氣對策信號',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                },
                labels: {
                    format: '{value} 分',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                }
            },
            {   //第二y軸
                title: {
                    text: '景氣指標'
                },
                opposite: true //把這一軸放到右邊
            },
            {   //第三y軸
                title: {
                    text: '台股加權股價指數',
                    style: {
                        color: Highcharts.getOptions().colors[5]
                    }
                },
                labels: {
                    format: '{value} 點',
                    style: {
                        color: Highcharts.getOptions().colors[5]
                    }
                },
                opposite: true //把這一軸放到右邊
            }
        ],
        options: {
            chart: {
                zoomType: 'xy'
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'middle',
                borderWidth: 0
            }
        },

        series: $scope.chartSeries
    };

    //聽從$rootScope廣播下來的Events
    $scope.$on('chartdata_created', function (event, args) {
        refreshChart();
    });

    $scope.$on('chartdata_updated', function (event, args) {
        refreshChart();
    });

    $scope.$on('chartdata_deleted', function (event, args) {
        refreshChart();
    });

    //重新refresh圖表
    refreshChart = function () {
        ChartDataFactory.resetTrainingDatas(function () {
            //***Highchart圖表要秀的資料***//
            //取得Highchart的chart物件instance
            var chart = $scope.chartConfig.getHighcharts();
            //重設每個資料序列的內容
            chart.series[0].setData(ChartDataFactory.getDataSeriesByProp("monitoringindex"), false);
            chart.series[1].setData(ChartDataFactory.getDataSeriesByProp("leadingindex"), false);
            chart.series[2].setData(ChartDataFactory.getDataSeriesByProp("coincidentindex"), false);
            chart.series[3].setData(ChartDataFactory.getDataSeriesByProp("laggingindex"), false);

            var mydata = ChartDataFactory.getDataSeriesByProp("taiex");
            chart.series[4].setData(ChartDataFactory.getDataSeriesByProp("taiex"), false);
            //要求Highchart重劃圖表
            chart.redraw();
        })
    };
});

// define a controller for 03_IntegrateGauage
myapp.controller('03_myapp_integrateGauageCtrl', function ($scope, ChartDataFactory, EventBusHub) {
    $scope.vm = {};
    $scope.vm.score = null;
    $scope.vm.light = null;
    $scope.vm.meaning = null;
    $scope.vm.period = null;

    // 景氣燈號的分數與意義
    var gauageDataMeaning = [
        { from: 9, to: 17, light: '藍燈', meaning: '景氣衰退' },
        { from: 17, to: 23, light: '黃藍燈', meaning: '景氣短期內有轉穩或趨於衰退之可能' },
        { from: 23, to: 32, light: '綠燈', meaning: '景氣穩定' },
        { from: 32, to: 38, light: '黃紅燈', meaning: '景氣尚穩, 但在短期內有轉熱或趨穩之可能' },
        { from: 38, to: 45, light: '紅燈', meaning: '景氣過熱, 政府可能採取緊縮措施' }
    ];    

    getLight = function(score){
        for(var i=0; i<gauageDataMeaning.length; i++){
            var meanObj = gauageDataMeaning[i];
            if (score >= meanObj.from && score < meanObj.to)
                return meanObj.light;
        }
    };

    getMeaning = function (score) {
        for (var i = 0; i < gauageDataMeaning.length; i++) {
            var meanObj = gauageDataMeaning[i];
            if (score >= meanObj.from && score < meanObj.to)
                return meanObj.meaning;
        }
    };   

    init = function () {
        ChartDataFactory.resetTrainingDatas(function () {
            var gauageData = ChartDataFactory.getLatesPeriodData();
            var myScore = gauageData.MonitoringIndex;
            $scope.vm.score = myScore;
            $scope.vm.light = getLight(myScore);
            $scope.vm.meaning = getMeaning(myScore);
            $scope.vm.period = gauageData.Period;
            //***Highchart圖表要秀的資料***//
            var chart = $scope.chartConfig.getHighcharts();
            chart.addSeries({
                name: "台灣景氣對策信號",
                data: [gauageData.MonitoringIndex],
                tooltip: {
                    valueSuffix: ' 分'
                }
            });
        })
    };

    init();

    //***Highchart的圖表configuration***//
    $scope.chartConfig = {
        title: {
            text: '台灣景氣對策信號'
        },
        // the value axis
        yAxis: {
            min: 9,
            max: 45,

            minorTickInterval: 'auto',
            minorTickWidth: 1,
            minorTickLength: 10,
            minorTickPosition: 'inside',
            minorTickColor: '#666',

            tickPixelInterval: 30,
            tickWidth: 2,
            tickPosition: 'inside',
            tickLength: 10,
            tickColor: '#666',
            labels: {
                step: 2,
                rotation: 'auto'
            },
            title: {
                text: '分'
            },
            plotBands: [
                {
                    from: 9,
                    to: 17,
                    color: '#87CEEB' // blue
                }, {
                    from: 17,
                    to: 23,
                    color: '#DDDF0D' // yellow
                }, {
                    from: 23,
                    to: 32,
                    color: '#55BF3B' // green
                }
                , {
                    from: 32,
                    to: 38,
                    color: '#FFA500' // yellow/red
                }, {
                    from: 38,
                    to: 45,
                    color: '#DF5353' // red
                }
            ]
        },
        options: {
            chart: {
                type: 'gauge',
                plotBackgroundColor: null,
                plotBackgroundImage: null,
                plotBorderWidth: 0,
                plotShadow: false
            },
            pane: {
                startAngle: -150,
                endAngle: 150,
                background: [{
                    backgroundColor: {
                        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                        stops: [
                            [0, '#FFF'],
                            [1, '#333']
                        ]
                    },
                    borderWidth: 0,
                    outerRadius: '109%'
                }, {
                    backgroundColor: {
                        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                        stops: [
                            [0, '#333'],
                            [1, '#FFF']
                        ]
                    },
                    borderWidth: 1,
                    outerRadius: '107%'
                }, {
                    // default background
                }, {
                    backgroundColor: '#DDD',
                    borderWidth: 0,
                    outerRadius: '105%',
                    innerRadius: '103%'
                }]
            },
        },
        series: $scope.chartSeries
    };
   

    //聽從$rootScope廣播下來的Event
    $scope.$on('chartdata_created', function (event, args) {
        refreshChart();
    });

    $scope.$on('chartdata_updated', function (event, args) {
        refreshChart();
    });

    $scope.$on('chartdata_deleted', function (event, args) {
        refreshChart();
    });

    refreshChart = function () {
        var chart = $scope.chartConfig.getHighcharts();

        ChartDataFactory.resetTrainingDatas(function () {
            var gauageData = ChartDataFactory.getLatesPeriodData();
            var myScore = gauageData.MonitoringIndex;
            $scope.vm.score = myScore;
            $scope.vm.light = getLight(myScore);
            $scope.vm.meaning = getMeaning(myScore);
            $scope.vm.period = gauageData.Period;

            chart.series[0].setData([gauageData.MonitoringIndex], true);
        });
    };
});

