document.addEventListener('DOMContentLoaded', function () {
    const track = document.getElementById("image-track");
    const header = document.querySelector('header');

    const getHeaderHeight = () => header.offsetHeight;

    const adjustSliderPosition = () => {
        const headerHeight = getHeaderHeight();
        const sliderHeight = track.offsetHeight;
        const newBottomPosition = window.innerHeight - headerHeight - sliderHeight - 100;
        track.style.bottom = `${newBottomPosition}px`;
    };

    adjustSliderPosition();
    window.addEventListener('resize', adjustSliderPosition);

    const handleOnDown = e => track.dataset.mouseDownAt = e.clientX;
    const handleOnUp = () => {
        track.dataset.mouseDownAt = "0";
        track.dataset.prevPercentage = track.dataset.percentage;
    }

    const handleOnMove = e => {
        if (track.dataset.mouseDownAt === "0") return;

        const mouseDelta = parseFloat(track.dataset.mouseDownAt) - e.clientX,
            maxDelta = window.innerWidth / 2;

        const percentage = (mouseDelta / maxDelta) * -100,
            nextPercentageUnconstrained = parseFloat(track.dataset.prevPercentage) + percentage,
            nextPercentage = Math.max(Math.min(nextPercentageUnconstrained, 0), -100);

        track.dataset.percentage = nextPercentage;

        track.animate({
            transform: `translate(${nextPercentage}%, -10%)`
        }, { duration: 50000, fill: "forwards" });

        for (const image of track.getElementsByClassName("image")) {
            image.animate({
                objectPosition: `${100 + nextPercentage}% center`
            }, { duration: 50000, fill: "forwards" });
        }
    }

    window.onmousedown = e => handleOnDown(e);
    window.ontouchstart = e => handleOnDown(e.touches[0]);
    window.onmouseup = e => handleOnUp(e);
    window.ontouchend = e => handleOnUp(e.touches[0]);
    window.onmousemove = e => handleOnMove(e);
    window.ontouchmove = e => handleOnMove(e.touches[0]);
});
