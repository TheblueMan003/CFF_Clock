from PIL import Image, ImageDraw

# create 4096x4096 image transparent image
hour = Image.new('RGBA', (4096, 4096), (0, 0, 0, 0))
# draw a trapazoid top 147 bottom 212 height 1884 with bottom center at (2048, 2048)
dhour = ImageDraw.Draw(hour)
top = 147
bottom = 212
height = 1884 + 491
shift = 491

dhour.polygon([(2048-top//2, 2048-height+ shift), (2048-bottom//2, 2048+ shift), (2048+bottom//2, 2048+ shift), (2048+top//2, 2048-height + shift)], fill=(0, 0, 0, 255))



hour.save('hand_hour.png')