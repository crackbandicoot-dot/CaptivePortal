systemctl stop hostapd
systemctl stop dnsmasq
ip addr del 192.168.4.1/24 dev wlp1s0

