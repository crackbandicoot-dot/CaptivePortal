systemctl stop hostapd
systemctl stop dnsmasq
ip addr del 192.168.4.1/24 dev wlp1s0
#sudo iptables -t nat -F
#sudo iptables -F
#sudo iptables -X
