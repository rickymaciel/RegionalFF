
var theme = {
    color: [
        '#26B99A', '#34495E', '#145626', '#132E40', '#9B59B6', '#075EC1', '#759c6a', '#00ABFF', '#FF5733', '#C70039'
    ],

    title: {
        itemGap: 10,
        textStyle: {
            fontWeight: 'normal',
            color: '#408829'
        }
    },

    dataRange: {
        color: ['#1f610a', '#97b58d']
    },

    toolbox: {
        color: ['#408829', '#408829', '#408829', '#408829']
    },

    tooltip: {
        backgroundColor: 'rgba(0,0,0,0.5)',
        axisPointer: {
            type: 'line',
            lineStyle: {
                color: '#408829',
                type: 'dashed'
            },
            crossStyle: {
                color: '#408829'
            },
            shadowStyle: {
                color: 'rgba(200,200,200,0.3)'
            }
        }
    },
};
var echartPie = echarts.init(document.getElementById('echart_pie'), theme);

echartPie.setOption({
    title : {
        text: Texto,
        subtext: '',
        x:'center'
    },
    tooltip: {
        trigger: 'item',
        formatter: "{a} <br/>{b} : {c} ({d}%)"
    },
    legend: {
        orient: 'vertical',
        left: 'left',
        data: LegendData
    },
    toolbox: {
        show: true,
        feature: {
            mark: { show: true },
            //dataView: { show: true, readOnly: false },
            magicType: {
                show: true,
                type: ['pie', 'funnel'],
                option: {
                    funnel: {
                        x: '25%',
                        width: '50%',
                        funnelAlign: 'center',
                        max: 1548
                    }
                }
            },
            restore: {
                show: true,
                title: "Restaurar"
            },
            saveAsImage: {
                show: true,
                title: "Guardar Imagen"
            }
        }
    },
    calculable: true,
    series: [{
        name: Titulo,
        type: 'pie',
        radius : '65%',
        center: ['50%', '60%'],
        data: SeriesData,
        itemStyle: {
            emphasis: {
                shadowBlur: 10,
                shadowOffsetX: 0,
                shadowColor: 'rgba(0, 0, 0, 0.5)'
            }
        }
    }]
});

var dataStyle = {
    normal: {
        label: {
            show: true
        },
        labelLine: {
            show: true
        }
    }
};

var placeHolderStyle = {
    normal: {
        color: 'rgba(0,0,0,0)',
        label: {
            show: true
        },
        labelLine: {
            show: true
        }
    },
    emphasis: {
        color: 'rgba(0,0,0,0)'
    }
};