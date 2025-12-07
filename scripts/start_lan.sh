
#Creates an AP
systemctl start hostapd
systemctl start dnsmasq
ip addr add 192.168.4.1/24 dev wlp1s0

#Allows routing
sysctl -w net.ipv4.ip_forward=1
iptables -t nat -A POSTROUTING -j MASQUERADE
