require_relative('./read.rb')

record = ARGV.empty? ? "000000" : ARGV.first
file_str = File.join(File.dirname(File.expand_path(__FILE__)), "..","data","record_#{record}.dat")
file = File.new(file_str)
reader = RecordReader.new(file)
user, messages = reader.read_record
puts user
puts
messages.each do |m|
  puts m
  puts
end