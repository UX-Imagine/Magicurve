/// <reference path="../../../typings/jquery/jquery.d.ts" />

interface IControl {
    X: number;
    Y: number;
    Height: number;
    Width: number;
    Type: string;
}

class Control implements IControl {
    X: number;
    Y: number;
    Height: number;
    Width: number;
    Type: string;

    constructor(x: number, y: number, height: number, width: number, shapeType: string) {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
        this.Type = shapeType;
    }
}
interface IControlResult {
    controls: IControl[];
    imageWidth: number;
    imageHeight: number;
    imagesUrl: string;
}

class ControlItem {
    public index: number;
    public control: any;
    constructor(index: number, item: any) {
        this.index = index;
        this.control = item;
    }
}

class Stage {
    public controls: Control[];
    public imageWidth: number;
    public imageHeight: number;
    public canvasHeight: number;
    public canvasWidth: number;
    public activeControlIndex: number;
    public currentImage: string;

    constructor(canvaswidth: number, canvasheight: number) {
        this.canvasHeight = canvasheight;
        this.canvasWidth = canvaswidth;
    }
}

