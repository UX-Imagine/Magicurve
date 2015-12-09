/// <reference path="../../../typings/jquery/jquery.d.ts" />

interface IControl {
    X: number;
    Y: number;
    Height: number;
    Width: number;
    Type: string;
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
    public controls: IControl[];
    public orginalControls: IControl[];
    public imageWidth: number;
    public imageHeight: number;
    public imagesUrl: string;
    public controlUrl: string;
    public codeUrl: string;
    public sourceUrl: string;
    public activeControlIndex: number;

    getControls(onSuccess: () => void): void {
        $.getJSON(this.controlUrl, data => {
            this.orginalControls = data.controls;
            this.controls = data.controls;
            this.imageWidth = data.imageWidth;
            this.imageHeight = data.imageHeight;
            onSuccess();
        });
    }

    getCode(onSuccess: () => any) {
        var self = this;
        $.ajax(
            {
                url: this.codeUrl,
                type: "POST",
                data: { controls: self.controls, imageWidth: self.imageWidth, imageHeight: self.imageHeight }
            }).done(data => {
                self.sourceUrl = data.url;
                onSuccess();
            }).fail(error => {
                console.log(error);
            });
    }
}

