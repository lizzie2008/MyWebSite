$("#bg-stars").attr("width", $(window).width());
$("#bg-stars").attr("height", $(window).height());

var BGStars = function()
{
    var _BGStars 	= this;
    this.STOPPED    = false;
    this.canvas 	= $('#bg-stars');
    this.NAME       = '#bg-stars';
    this.stage 		= new PIXI.Stage(0x000000);
    this.renderer 	= PIXI.autoDetectRenderer(this.canvas.width(), this.canvas.height(), this.canvas.get(0), true, true);
    this.NUM_POINTS = 12;
    this.points = [];
    this.radius = 2;
    this.width = this.canvas.width(),
    this.height = this.canvas.height(),

    this.init = function(){

        // ADD GRAPHICS
        gr = new PIXI.Graphics();
        _BGStars.stage.addChild(gr)

        // CREATE POINTS
        for(var i = 0; i < _BGStars.NUM_POINTS; i++)
        {
            var x = Math.random() * _BGStars.width
                , y = Math.random() * _BGStars.height
                , vX = this.randBtwn(-3, 3)
                , vY = this.randBtwn(-3, 3)
                , radius = this.randBtwn(2, 3);

            _BGStars.points.push({
                x: x,
                y: y,
                vY: vY,
                vX: vX,
                vY0: vY,
                vX0: vX,
                radius: radius
            })
        }

        _BGStars.animate();
    }

    this.animate = function(){
        requestAnimFrame( _BGStars.animate );

        //if(_BGStars.STOPPED)
        //    return;

        gr.clear();
        gr.beginFill(0xFFFFFF);

        var point, i;
        for(i = 0; i < _BGStars.points.length; i++)
        {
            point = _BGStars.points[i];
            gr.drawCircle(point.x, point.y, point.radius);
        }

        _BGStars.points.forEach(function(point)
        {
            if(point.y > (_BGStars.height - _BGStars.radius) || point.y < _BGStars.radius)
            {
                point.vY *= -1;
            }

            if(point.x > (_BGStars.width - _BGStars.radius) || point.x < _BGStars.radius)
            {
                point.vX *= -1;
            }

            point.x += point.vX;
            point.y += point.vY;
        });


        gr.endFill();

        // render the stage
        _BGStars.renderer.render(_BGStars.stage);
    }

    this.randBtwn = function(min, max)
    {
        return min + (max - min) * Math.random();
    }

    this.stop = function()
    {
        console.log('BGSTARS STOPPING')
        _BGStars.STOPPED = true;
    }

    this.play = function()
    {
        console.log('BGSTARS STARTING')
        _BGStars.STOPPED = false;
    }
}


if(!isMobile){
    var BG = new BGStars;
    BG.init();
}